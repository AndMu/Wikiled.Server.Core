using Microsoft.AspNetCore.Builder;

namespace Wikiled.Server.Core.Middleware
{
    public static class LoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}
