using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Wikiled.Core.Standard.Api.Server;
using Wikiled.Core.Standard.Arguments;

namespace Wikiled.Server.Core.Responses
{
    public class InvalidViewStateResponse : ApiResponse
    {
        private readonly string status;

        public InvalidViewStateResponse(ModelStateDictionary modelState)
            : base(400)
        {
            Guard.NotNull(() => modelState, modelState);
            Guard.IsValid(() => modelState, modelState, dictionary => !dictionary.IsValid, "ModelState must be invalid");
            Errors = modelState.SelectMany(item => item.Value.Errors).Select(item => item.ErrorMessage).ToArray();
            status = GetStatus();
        }

        public InvalidViewStateResponse(string[] errors)
            : base(400)
        {
            Errors = errors;
            status = GetStatus();
        }

        public string[] Errors { get; }

        
        public override string Status => status;

        private string GetStatus()
        {
            StringBuilder errorBuilder = new StringBuilder();
            errorBuilder.Append("Serialization Error");

            if (Errors == null || Errors.Length == 0)
            {
                return errorBuilder.ToString();
            }

            errorBuilder.Append(":");
            foreach (var error in Errors)
            {
                errorBuilder.Append($" {error}");
                errorBuilder.Append(";");
            }

            return errorBuilder.ToString();
        }
    }
}
