using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Wikiled.Core.Standard.Api.Server;
using Wikiled.Core.Standard.Arguments;

namespace Wikiled.Server.Core.Responses
{
    public class InvalidViewStateResponse : ApiResponse
    {
        public InvalidViewStateResponse(ModelStateDictionary modelState)
            : base(400, "Serialization Error")
        {
            Guard.NotNull(() => modelState, modelState);
            Guard.IsValid(() => modelState, modelState, dictionary => !dictionary.IsValid, "ModelState must be invalid");
            Errors = modelState.SelectMany(item => item.Value.Errors).Select(item => item.ErrorMessage).ToArray();
        }

        public InvalidViewStateResponse(string[] errors)
            : base(400, "Serialization Error")
        {
            Errors = errors;
        }

        public string[] Errors { get; }
    }
}
