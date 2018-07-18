using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Wikiled.Server.Core.Helpers;

namespace Wikiled.Server.Core.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestLoggingMiddleware>();
        }

        public Task Invoke(HttpContext context)
        {
            var request = context.Request;
            _logger.LogInformation($"{new IpResolve(context).GetRequestIp()} {request.Scheme} {request.Host}{request.Path} {request.QueryString}");
            return _next(context);
        }
    }
}
