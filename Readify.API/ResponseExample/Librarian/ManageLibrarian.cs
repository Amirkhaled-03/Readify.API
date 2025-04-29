namespace Readify.API.ResponseExample.Librarian
{
    using global::Readify.API.HandleResponses;
    using global::Readify.BLL.Features.Librarian.DTOs;
    using global::Readify.BLL.Helpers;
    using Swashbuckle.AspNetCore.Filters;

    namespace Readify.API.ResponseExample.Librarian
    {
        public class ManageLibrarianPageExample : IExamplesProvider<ApiResponse<ManageLibrarianPageDto>>
        {
            public ApiResponse<ManageLibrarianPageDto> GetExamples()
            {
                return new ApiResponse<ManageLibrarianPageDto>(200, "success", data: new ManageLibrarianPageDto
                {
                    Librarians = new List<LibrarianDto>
                {
                    new LibrarianDto
                    {
                        Id = "58856-dd-gaaaa-cc",
                        Fullname = "John Doe",
                        Username = "johndoe",
                        PhoneNumber = "123456789",
                        UserStatus = UserStatus.Approved,
                        CreatedAt = DateTime.UtcNow.AddDays(-30)
                    },
                    new LibrarianDto
                    {
                        Id = "58856-dd-gaaaa-cssssc",
                        Fullname = "Jane Smith",
                        Username = "janesmith",
                        PhoneNumber = "987654321",
                        UserStatus = UserStatus.Pending,
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
        }

        public class EditLibrarianSuccessExample : IExamplesProvider<ApiResponse<List<string>>>
        {
            public ApiResponse<List<string>> GetExamples()
            {
                return new ApiResponse<List<string>>(200, "Librarian updated successfully", new List<string>());
            }
        }

        public class EditLibrarianFailedExample : IExamplesProvider<ApiResponse<List<string>>>
        {
            public ApiResponse<List<string>> GetExamples()
            {
                return new ApiResponse<List<string>>(400, "Failed to update librarian", new List<string>
            {
                "Phone number already used by another account.",
                "Username already exists.",
                "This Librarian does not exist.",
                "An error occurred while updating the librarian."
            });
            }
        }

        public class DeleteLibrarianSuccessExample : IExamplesProvider<ApiResponse<List<string>>>
        {
            public ApiResponse<List<string>> GetExamples()
            {
                return new ApiResponse<List<string>>(200, "Librarian deleted successfully", new List<string>());
            }
        }

        public class DeleteLibrarianFailedExample : IExamplesProvider<ApiResponse<List<string>>>
        {
            public ApiResponse<List<string>> GetExamples()
            {
                return new ApiResponse<List<string>>(400, "Failed to delete librarian", new List<string>
            {
                "This librarian does not exist."
            });
            }
        }

        public class GetLibrarianByIdSuccessExample : IExamplesProvider<ApiResponse<LibrarianDto>>
        {
            public ApiResponse<LibrarianDto> GetExamples()
            {
                return new ApiResponse<LibrarianDto>(200, "success", new LibrarianDto
                {
                    Id = "12345",
                    Fullname = "John Doe",
                    Username = "johndoe",
                    PhoneNumber = "123456789",
                    UserStatus = UserStatus.Approved,
                    CreatedAt = DateTime.UtcNow.AddDays(-50)
                });
            }
        }

        public class GetLibrarianByIdNotFoundExample : IExamplesProvider<ApiResponse<object>>
        {
            public ApiResponse<object> GetExamples()
            {
                return new ApiResponse<object>(404, "Librarian not found.");
            }
        }
    }
}
