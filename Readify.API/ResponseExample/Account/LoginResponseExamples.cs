using Readify.API.HandleResponses;
using Swashbuckle.AspNetCore.Filters;

namespace Readify.API.ResponseExample.Account
{
    public class LoginSuccessResponseExample : IExamplesProvider<ApiResponse<Dictionary<string, string>>>
    {
        public ApiResponse<Dictionary<string, string>> GetExamples()
        {
            return new ApiResponse<Dictionary<string, string>>(200, "success", new Dictionary<string, string>
        {
            { "token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6ImQxMjQ4YWIyLWFlYmQtNDM1ZC1iZWY1LTcyNzgzMDg3MmQ4NyIsInJvbGUiOiJBZG1pbiIsIm5iZiI6MTc0NTE5MTg3OCwiZXhwIjoxNzQ3NzgzODc4LCJpYXQiOjE3NDUxOTE4NzgsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcxODMiLCJhdWQiOiJZb3VyQXVkaWVuY2VlZWVlIn0.l1RE4_oEiXiAc3c-aZfYOFXvwRuC61BEr4ksVICPMDU" }
        });
        }
    }

    public class LoginErrorResponseExample : IExamplesProvider<ApiResponse<IReadOnlyList<string>>>
    {
        public ApiResponse<IReadOnlyList<string>> GetExamples()
        {
            return new ApiResponse<IReadOnlyList<string>>(400, "bad request", errors:
               new List<string>() { "Invalid email or password.", "....", "Email is required" }
            );
        }
    }
}
