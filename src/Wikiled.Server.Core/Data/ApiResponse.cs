using Newtonsoft.Json;

namespace Wikiled.Server.Core.Data
{
    public class ApiResponse
    {
        public ApiResponse(int code, string message = null)
        {
            Code = code;
            ResponseType = code == 200 ? ResponseType.Ok : ResponseType.Error;
            Status = message ?? GetDefaultMessageForStatusCode(code);
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; }

        public int Code { get; }

        public ResponseType ResponseType { get; }

        private static string GetDefaultMessageForStatusCode(int statusCode)
        {
            switch (statusCode)
            {
                case 200:
                    return string.Empty;
                case 404:
                    return "Resource not found";
                case 500:
                    return "An unhandled error occurred";
                default:
                    return "Unknown Error";
            }
        }
    }
}
