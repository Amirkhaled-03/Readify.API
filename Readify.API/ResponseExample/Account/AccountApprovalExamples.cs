using Readify.API.HandleResponses;
using Readify.BLL.Features.Account.DTOs.AccountApproval;
using Readify.BLL.Helpers;
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

    public class GetPendingAccountsExample : IExamplesProvider<ApiResponse<ManageAcceptsAccountsPageDto>>
    {
        public ApiResponse<ManageAcceptsAccountsPageDto> GetExamples()
        {
            return new ApiResponse<ManageAcceptsAccountsPageDto>(200, "success", new ManageAcceptsAccountsPageDto
            {
                Accounts = new List<AcceptAccountsDto>
                {
                    new AcceptAccountsDto
                    {
                        Id = "u001",
                        Fullname = "Sarah Ahmed",
                        Email = "sarah@domain.com",
                        UserStatus = UserStatus.Pending.ToString(),
                        UserType = UserType.Librarian.ToString()
                    },
                    new AcceptAccountsDto
                    {
                        Id = "u002",
                        Fullname = "Ali Hossam",
                        Email = "ali@domain.com",
                        UserStatus = UserStatus.Pending.ToString(),
                        UserType = UserType.User.ToString(),
                    }
                },
                Metadata = new Metadata
                {
                    Pagination = new Pagination
                    {
                        PageIndex = 1,
                        PageSize = 10,
                        TotalRecords = 2,
                        TotalPages = 1
                    }
                }
            });
        }
    }
}
