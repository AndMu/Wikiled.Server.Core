using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Wikiled.Core.Standard.Arguments;

namespace Wikiled.Server.Core.Responses
{
    public class ValidationResult
    {
        public ValidationResult(ModelStateDictionary modelState)
        {
            Guard.NotNull(() => modelState, modelState);
            Message = "Validation Failed";
            Errors = modelState.Keys
                               .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                               .ToArray();
        }

        [JsonConstructor]
        public ValidationResult(ValidationError[] errors, string message)
        {
            Errors = errors;
            Message = message;
        }

        public ValidationError[] Errors { get; }

        public string Message { get; }
    }
}
