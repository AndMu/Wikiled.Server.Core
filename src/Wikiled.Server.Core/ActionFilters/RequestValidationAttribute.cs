using System.Text;
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
                var result = new InvalidViewStateResponse(context.ModelState);
                context.Result = result;
            }

            base.OnActionExecuting(context);
        }
    }
}
