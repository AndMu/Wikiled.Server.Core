using Newtonsoft.Json;

namespace Wikiled.Service.Data
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Status = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; }

        public int StatusCode { get; }

        private static string GetDefaultMessageForStatusCode(int statusCode)
        {
            switch (statusCode)
            {
                case 404: return "Resource not found";
                case 500: return "An unhandled error occurred";
                default: return null;
            }
        }
    }
}
