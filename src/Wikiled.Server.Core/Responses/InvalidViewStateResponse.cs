using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NLog;

namespace Wikiled.Server.Core.Responses
{
    public class InvalidViewStateResponse : ObjectResult
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public InvalidViewStateResponse(ModelStateDictionary modelState)
            : base(new ValidationResult(modelState))
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity;
        }

        public override void ExecuteResult(ActionContext context)
        {
            var result = Value as ValidationResult;
            logger.Error(result);
            base.ExecuteResult(context);
        }
    }
}
