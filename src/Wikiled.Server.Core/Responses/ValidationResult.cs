using System;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Wikiled.Server.Core.Responses
{
    public class ValidationResult
    {
        public ValidationResult(ModelStateDictionary modelState)
        {
            if (modelState == null)
            {
                throw new ArgumentNullException(nameof(modelState));
            }

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

        public override string ToString()
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
