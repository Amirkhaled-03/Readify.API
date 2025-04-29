using Readify.API.HandleResponses;
using Readify.BLL.Features.Librarian.DTOs;
using Readify.BLL.Helpers;
using Swashbuckle.AspNetCore.Filters;

namespace Readify.API.ResponseExample.User
{
    public class ManageUsersPageExample : IExamplesProvider<ApiResponse<ManageLibrarianPageDto>>
    {
        public ApiResponse<ManageLibrarianPageDto> GetExamples()
        {
            return new ApiResponse<ManageLibrarianPageDto>(200, "success", data: new ManageLibrarianPageDto
            {
                Librarians = new List<LibrarianDto>
                {
                    new LibrarianDto
                    {
                        Id = "ssss-aaaa-fff-eeee",
                        Fullname = "John Doe",
                        Username = "johndoe",
                        PhoneNumber = "123456789",
                        UserStatus = UserStatus.Pending,
                        CreatedAt = DateTime.UtcNow.AddDays(-30)
                    },
                    new LibrarianDto
                    {
                        Id = "ssss-aaaaddd-fff-eeee",
                        Fullname = "Jane Smith",
                        Username = "janesmith",
                        PhoneNumber = "987654321",
                        UserStatus = UserStatus.Rejected,
                        CreatedAt = DateTime.UtcNow.AddDays(-60)
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

        public class EditUserSuccessExample : IExamplesProvider<ApiResponse<List<string>>>
        {
            public ApiResponse<List<string>> GetExamples()
            {
                return new ApiResponse<List<string>>(200, "User updated successfully", new List<string>());
            }
        }

        public class EditUserFailedExample : IExamplesProvider<ApiResponse<List<string>>>
        {
            public ApiResponse<List<string>> GetExamples()
            {
                return new ApiResponse<List<string>>(400, "Failed to update user", new List<string>
        {
            "Phone number already used by another account.",
            "Username already exists.",
            "This User does not exist.",
            "An error occurred while updating the user."
        });
            }
        }

        public class DeleteUserSuccessExample : IExamplesProvider<ApiResponse<List<string>>>
        {
            public ApiResponse<List<string>> GetExamples()
            {
                return new ApiResponse<List<string>>(200, "User deleted successfully", new List<string>());
            }
        }

        public class DeleteUserFailedExample : IExamplesProvider<ApiResponse<List<string>>>
        {
            public ApiResponse<List<string>> GetExamples()
            {
                return new ApiResponse<List<string>>(400, "Failed to delete user", new List<string>
            {
                "This user does not exist."
            });
            }
        }

        public class GetUserByIdSuccessExample : IExamplesProvider<ApiResponse<LibrarianDto>>
        {
            public ApiResponse<LibrarianDto> GetExamples()
            {
                return new ApiResponse<LibrarianDto>(200, "success", new LibrarianDto
                {
                    Id = "12345",
                    Fullname = "Jane Doe",
                    Username = "janedoe",
                    PhoneNumber = "987654321",
                    UserStatus = UserStatus.Approved,
                    CreatedAt = DateTime.UtcNow.AddDays(-25)
                });
            }
        }

        public class GetUserByIdNotFoundExample : IExamplesProvider<ApiResponse<object>>
        {
            public ApiResponse<object> GetExamples()
            {
                return new ApiResponse<object>(404, "User not found.");
            }
        }
    }
}