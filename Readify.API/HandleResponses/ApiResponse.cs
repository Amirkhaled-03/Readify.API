namespace Readify.API.HandleResponses
{
    public class ApiResponse<T>
    {
        public int Status { get; set; }
        public string? Message { get; set; }
        public T Data { get; set; }
        public object Errors { get; set; }

        public ApiResponse(int statusCode, string message, T data = default, object errors = null)
        {
            Status = statusCode;
            Message = message ?? GetErrorMessageForStatusCode(statusCode);
            Data = data;
            Errors = errors;
        }

        private string? GetErrorMessageForStatusCode(int code)
        {
            return code switch
            {
                200 => "Success",                          // OK
                201 => "Created",                          // Resource created successfully
                302 => "Redirect",                         // 
                400 => "Bad Request",                      // Invalid request from the client
                401 => "Unauthorized",                     // Unauthorized
                403 => "Forbidden",                        // Server refuses to fulfill the request
                404 => "Resource not found!",              // Requested resource not found
                500 => "Internal Server Error",            // A generic server error
                502 => "Bad Gateway",                      // Invalid response from an upstream server
                503 => "Service Unavailable",              // The server is temporarily unavailable
                _ => "Unknown Error"                       // For any other status code
            };
        }

    }
}
