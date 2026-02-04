namespace ERP.Data
{
    /// <summary>
    /// Provides a fresh AppDbContext for the current sandbox session.
    /// Registered as Scoped - creates ONE context per HTTP request.
    /// Implements IDisposable to properly close the connection at end of request.
    /// </summary>
    public class SandboxDbContextProvider : IDisposable
    {
        private readonly SandboxDbContextFactory _factory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private AppDbContext? _context;
        private bool _disposed;

        public SandboxDbContextProvider(SandboxDbContextFactory factory, IHttpContextAccessor httpContextAccessor)
        {
            _factory = factory;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Gets or creates the DbContext for this request.
        /// Returns the same instance within a single HTTP request (scoped).
        /// </summary>
        public AppDbContext GetContext()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(SandboxDbContextProvider));
                
            // Return cached context if already created for this request
            if (_context != null)
            {
                return _context;
            }

            var httpContext = _httpContextAccessor.HttpContext 
                ?? throw new InvalidOperationException("No HTTP context available");

            var sessionId = httpContext.Items["SandboxSessionId"] as string;
            
            if (string.IsNullOrEmpty(sessionId))
            {
                // Fallback: create a temporary session
                sessionId = $"sandbox_{Guid.NewGuid():N}";
                httpContext.Items["SandboxSessionId"] = sessionId;
            }

            // Create a fresh context for this request
            _context = _factory.CreateContext(sessionId);
            return _context;
        }

        public TimeSpan? GetTimeRemaining()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var sessionId = httpContext?.Items["SandboxSessionId"] as string;
            
            if (string.IsNullOrEmpty(sessionId))
                return null;
                
            return _factory.GetTimeRemaining(sessionId);
        }
        
        public void Dispose()
        {
            if (!_disposed)
            {
                // Dispose the context which will close its connection
                _context?.Dispose();
                _context = null;
                _disposed = true;
            }
        }
    }
}
