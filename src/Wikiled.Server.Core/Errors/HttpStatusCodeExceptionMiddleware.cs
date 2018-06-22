using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Wikiled.Server.Core.Errors
{
    public class HttpStatusCodeExceptionMiddleware
    {
        private readonly RequestDelegate next;

        private readonly ILogger<HttpStatusCodeExceptionMiddleware> logger;

        public HttpStatusCodeExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            logger = loggerFactory?.CreateLogger<HttpStatusCodeExceptionMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (HttpStatusCodeException ex)
            {
                if (context.Response.HasStarted)
                {
                    logger.LogWarning("The response has already started, the http status code middleware will not be executed.");
                    throw;
                }

                context.Response.Clear();
                context.Response.StatusCode = ex.StatusCode;
                context.Response.ContentType = ex.ContentType;
                await context.Response.WriteAsync(ex.Message);
            }
        }
    }
}
