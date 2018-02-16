using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Wikiled.Server.Core.Responses;

namespace Wikiled.Server.Core.ActionFilters
{
    public class RequestValidationAttribute : ActionFilterAttribute
    {
        private readonly ILogger logger;

        public RequestValidationAttribute(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<RequestValidationAttribute>();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                logger.LogError("Invalid View state detected");
                var response = new InvalidViewStateResponse(context.ModelState);
                logger.LogError(response.Value?.ToString());
                context.Result = response;
            }

            base.OnActionExecuting(context);
        }
    }
}
