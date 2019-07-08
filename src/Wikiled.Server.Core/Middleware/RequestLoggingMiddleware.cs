using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Wikiled.Server.Core.Helpers;

namespace Wikiled.Server.Core.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate next;

        private readonly ILogger logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this.next = next;
            logger = loggerFactory.CreateLogger<RequestLoggingMiddleware>();
        }

        public Task Invoke(HttpContext context)
        {
            var request = context.Request;
            logger.LogInformation($"{new IpResolve(context).GetRequestIp()} {request.Scheme} {request.Host}{request.Path} {request.QueryString}");
            return next(context);
        }
    }
}
