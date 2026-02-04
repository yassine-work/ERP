using ERP.Data;

namespace ERP.Middleware
{
    /// <summary>
    /// Middleware that manages sandbox sessions for each visitor.
    /// Creates a unique session ID and stores it in a cookie.
    /// </summary>
    public class SandboxSessionMiddleware
    {
        private readonly RequestDelegate _next;
        private const string SessionCookieName = "ERP_SandboxSession";
        private const int SessionDurationMinutes = 20;

        public SandboxSessionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, SandboxDbContextFactory sandboxFactory)
        {
            // Skip for static files
            if (context.Request.Path.StartsWithSegments("/lib") ||
                context.Request.Path.StartsWithSegments("/css") ||
                context.Request.Path.StartsWithSegments("/js") ||
                context.Request.Path.StartsWithSegments("/favicon"))
            {
                await _next(context);
                return;
            }

            // Get or create session ID
            string? sessionId = context.Request.Cookies[SessionCookieName];
            bool isNewSession = false;
            
            // Create new session if doesn't exist or expired
            if (string.IsNullOrEmpty(sessionId) || !sandboxFactory.SessionExists(sessionId))
            {
                sessionId = $"sandbox_{Guid.NewGuid():N}";
                isNewSession = true;
                
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = context.Request.IsHttps,
                    SameSite = SameSiteMode.Lax,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(SessionDurationMinutes)
                };
                
                context.Response.Cookies.Append(SessionCookieName, sessionId, cookieOptions);
            }

            // Store session ID in HttpContext for use by other services
            context.Items["SandboxSessionId"] = sessionId;
            
            // Ensure the session database is initialized before continuing
            // This triggers session creation if it doesn't exist
            sandboxFactory.EnsureSessionExists(sessionId);
            
            // Now get the accurate time remaining from the server
            var timeRemaining = sandboxFactory.GetTimeRemaining(sessionId);
            context.Items["SandboxTimeRemaining"] = timeRemaining;

            await _next(context);
        }
    }

    public static class SandboxSessionMiddlewareExtensions
    {
        public static IApplicationBuilder UseSandboxSession(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SandboxSessionMiddleware>();
        }
    }
}
