namespace Wikiled.Server.Core.Data
{
    public class ErrorResponse : ApiResponse
    {
        public ErrorResponse(string message = null)
            : base(400, message)
        {
        }
    }
}
