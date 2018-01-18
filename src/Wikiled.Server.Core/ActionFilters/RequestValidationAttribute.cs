using Microsoft.AspNetCore.Mvc.Filters;
using Wikiled.Server.Core.Responses;

namespace Wikiled.Server.Core.ActionFilters
{
    public class RequestValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var result = new InvalidViewStateResponse(context.ModelState);
                context.Result = result;
            }

            base.OnActionExecuting(context);
        }
    }
}
