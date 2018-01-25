using Microsoft.AspNetCore.Mvc.Filters;
using NLog;
using Wikiled.Server.Core.Responses;

namespace Wikiled.Server.Core.ActionFilters
{
    public class RequestValidationAttribute : ActionFilterAttribute
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                logger.Error("Invlid View state detected");
                var response = new InvalidViewStateResponse(context.ModelState);
                logger.Error(response.Value);
                context.Result = response;
            }

            base.OnActionExecuting(context);
        }
    }
}
