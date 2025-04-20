namespace Readify.API.HandleResponses
{
    public class ApiExceptionResponse : ApiResponse<string>
    {
        public string Details { get; set; }
        public ApiExceptionResponse(int statusCode, string message = null, string details = null)
            : base(statusCode, message)
        {
            Details = details;
        }
    }
}
