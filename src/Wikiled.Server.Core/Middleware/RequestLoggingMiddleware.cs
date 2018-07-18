using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Wikiled.Server.Core.Helpers;

namespace Wikiled.Server.Core.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger _logger;

        private IIpResolve resolver;

        public RequestLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IIpResolve resolver)
        {
            _next = next;
            this.resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
            _logger = loggerFactory.CreateLogger<RequestLoggingMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation(await FormatRequest(context.Request).ConfigureAwait(false));
            await _next(context).ConfigureAwait(false);
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;
            request.EnableRewind();
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body = body;

            return $"{resolver.GetRequestIp()} {request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }
    }
}
