using System.Linq;
using System.Text;
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

        public string[] Errors { get; }

        public override string ToString()
        {
            if (Errors == null || Errors.Length == 0)
            {
                return "Error in request";
            }

            StringBuilder errorBuilder = new StringBuilder();
            errorBuilder.Append("Error in request:");
            foreach (var error in Errors)
            {
                errorBuilder.Append($" {error}");
                errorBuilder.Append(";");
            }

            return base.ToString();
        }
    }
}
