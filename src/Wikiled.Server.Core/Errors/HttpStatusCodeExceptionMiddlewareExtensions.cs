using Microsoft.AspNetCore.Builder;

namespace Wikiled.Server.Core.Errors
{
    public static class HttpStatusCodeExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpStatusCodeExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpStatusCodeExceptionMiddleware>();
        }
    }
}