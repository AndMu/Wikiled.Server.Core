using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Wikiled.Core.Standard.Arguments;

namespace Wikiled.Server.Core.Responses
{
    public class ValidationResultModel
    {
        public ValidationResultModel(ModelStateDictionary modelState)
        {
            Guard.NotNull(() => modelState, modelState);
            Message = "Validation Failed";
            Errors = modelState.Keys
                               .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                               .ToArray();
        }

        [JsonConstructor]
        public ValidationResultModel(ValidationError[] errors, string message)
        {
        }

        public ValidationError[] Errors { get; }

        public string Message { get; }
    }
}
