using Readify.API.HandleResponses;
using Swashbuckle.AspNetCore.Filters;

namespace Readify.API.ResponseExample.Account
{
    public class UpdateAccountStatusSuccessExample : IExamplesProvider<ApiResponse<string>>
    {
        public ApiResponse<string> GetExamples()
        {
            return new ApiResponse<string>(200, "Status updated successfully", data: "User status updated.");
        }
    }

    public class UpdateAccountStatusErrorExample : IExamplesProvider<ApiResponse<List<string>>>
    {
        public ApiResponse<List<string>> GetExamples()
        {
            return new ApiResponse<List<string>>(400, "Bad request", errors: new List<string>
        {
            "User not found!",
            "Invalid status value."
        });
        }
    }
}
