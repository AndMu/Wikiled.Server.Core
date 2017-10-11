using Microsoft.AspNetCore.Mvc;
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
                context.Result = new BadRequestObjectResult(new InvalidViewStateResponse(context.ModelState));
            }

            base.OnActionExecuting(context);
        }
    }
}
