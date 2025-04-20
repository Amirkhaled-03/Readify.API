using Readify.API.HandleResponses;
using Swashbuckle.AspNetCore.Filters;

namespace Readify.API.ResponseExample.Account
{
    public class RegisterAdminSuccessExample : IExamplesProvider<ApiResponse<string>>
    {
        public ApiResponse<string> GetExamples()
        {
            return new ApiResponse<string>(201, "success");
        }
    }
    public class RegisterAdminErrorExample : IExamplesProvider<ApiResponse<List<string>>>
    {
        public ApiResponse<List<string>> GetExamples()
        {
            return new ApiResponse<List<string>>(400, "bad request", errors: new List<string>
        {
            "Email is already taken.",
            "Password must be at least 8 characters.",
            "A user with the email user@exmaple.com already exists, enter other Email.",
            "...."
        });
        }
    }

}
