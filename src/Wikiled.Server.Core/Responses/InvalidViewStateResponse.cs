using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Wikiled.Server.Core.Responses
{
    public class InvalidViewStateResponse : ObjectResult
    {
        public InvalidViewStateResponse(ModelStateDictionary modelState)
            : base(new ValidationResult(modelState))
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity;
        }
    }
}
