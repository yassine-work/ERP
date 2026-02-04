using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using System.Collections.Concurrent;

namespace ERP.Data
{
    /// <summary>
    /// Manages per-session in-memory databases for sandbox/demo mode.
    /// Each visitor gets their own isolated database that expires after a timeout.
    /// 
    /// Key insight: SQLite in-memory DBs persist as long as ONE connection stays open.
    /// We keep a "keeper" connection open, but create fresh DbContext per request.
    /// </summary>
    public class SandboxDbContextFactory : IDisposable
    {
        private readonly ConcurrentDictionary<string, SandboxSession> _sessions = new();
        private readonly TimeSpan _sessionTimeout;
        private readonly Timer _cleanupTimer;

        public SandboxDbContextFactory(TimeSpan? sessionTimeout = null)
        {
            _sessionTimeout = sessionTimeout ?? TimeSpan.FromMinutes(20);
            
            // Run cleanup every 2 minutes
            _cleanupTimer = new Timer(CleanupExpiredSessions, null, TimeSpan.FromMinutes(2), TimeSpan.FromMinutes(2));
        }

        /// <summary>
        /// Creates a NEW DbContext for the session. The caller owns this context.
        /// </summary>
        public AppDbContext CreateContext(string sessionId)
        {
            var session = _sessions.GetOrAdd(sessionId, CreateNewSession);
            session.LastAccessed = DateTime.UtcNow;

            // Create a NEW connection to the same in-memory database
            // The keeper connection keeps the DB alive, this connection is for actual work
            var connection = new SqliteConnection(session.ConnectionString);
            connection.Open();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(connection)
                .Options;

            var context = new AppDbContext(options);
            
            // Log for debugging
            Console.WriteLine($"[Sandbox] Created context for session: {sessionId} (Active sessions: {_sessions.Count})");
            
            return context;
        }

        public DateTime? GetSessionExpiry(string sessionId)
        {
            if (_sessions.TryGetValue(sessionId, out var session))
            {
                return session.CreatedAt.Add(_sessionTimeout);
            }
            return null;
        }

        public TimeSpan? GetTimeRemaining(string sessionId)
        {
            var expiry = GetSessionExpiry(sessionId);
            if (expiry.HasValue)
            {
                var remaining = expiry.Value - DateTime.UtcNow;
                return remaining > TimeSpan.Zero ? remaining : TimeSpan.Zero;
            }
            return null;
        }

        /// <summary>
        /// Ensures a session exists without returning a context.
        /// Used by middleware to initialize session before controller runs.
        /// </summary>
        public void EnsureSessionExists(string sessionId)
        {
            // GetOrAdd will create the session if it doesn't exist
            _sessions.GetOrAdd(sessionId, CreateNewSession);
        }

        public bool SessionExists(string sessionId) => _sessions.ContainsKey(sessionId);

        public int ActiveSessionCount => _sessions.Count;

        private SandboxSession CreateNewSession(string sessionId)
        {
            // Connection string for shared in-memory database
            var connectionString = $"Data Source={sessionId};Mode=Memory;Cache=Shared";
            
            // Create and keep open a "keeper" connection - this keeps the in-memory DB alive
            var keeperConnection = new SqliteConnection(connectionString);
            keeperConnection.Open();

            // Create initial context to set up schema and seed data
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(connectionString)
                .Options;

            using (var setupContext = new AppDbContext(options))
            {
                setupContext.Database.EnsureCreated();
                RuntimeDataSeeder.SeedAllData(setupContext);
            }

            Console.WriteLine($"[Sandbox] Created new session: {sessionId}");

            return new SandboxSession
            {
                SessionId = sessionId,
                ConnectionString = connectionString,
                KeeperConnection = keeperConnection,
                CreatedAt = DateTime.UtcNow,
                LastAccessed = DateTime.UtcNow
            };
        }

        private void CleanupExpiredSessions(object? state)
        {
            var expiredSessions = _sessions
                .Where(kvp => DateTime.UtcNow - kvp.Value.CreatedAt > _sessionTimeout)
                .Select(kvp => kvp.Key)
                .ToList();

            foreach (var sessionId in expiredSessions)
            {
                if (_sessions.TryRemove(sessionId, out var session))
                {
                    try
                    {
                        // Close the keeper connection - this destroys the in-memory database
                        session.KeeperConnection?.Close();
                        session.KeeperConnection?.Dispose();
                        Console.WriteLine($"[Sandbox] Cleaned up expired session: {sessionId}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[Sandbox] Error cleaning up session {sessionId}: {ex.Message}");
                    }
                }
            }

            if (expiredSessions.Count > 0)
            {
                Console.WriteLine($"[Sandbox] Cleaned up {expiredSessions.Count} expired sessions. Active: {_sessions.Count}");
            }
        }

        public void Dispose()
        {
            _cleanupTimer?.Dispose();
            foreach (var session in _sessions.Values)
            {
                try
                {
                    session.KeeperConnection?.Close();
                    session.KeeperConnection?.Dispose();
                }
                catch { }
            }
            _sessions.Clear();
        }

        private class SandboxSession
        {
            public required string SessionId { get; set; }
            public required string ConnectionString { get; set; }
            public required SqliteConnection KeeperConnection { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime LastAccessed { get; set; }
        }
    }
}
