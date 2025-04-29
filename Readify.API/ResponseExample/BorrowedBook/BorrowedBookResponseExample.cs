using Readify.API.HandleResponses;
using Readify.BLL.Features.BorrowedBooks.DTOs;
using Readify.BLL.Helpers;
using Swashbuckle.AspNetCore.Filters;

namespace Readify.API.ResponseExample.BorrowedBook
{
    public class BorrowedBookResponseExample : IExamplesProvider<ApiResponse<ManageBorrowedBooksDto>>
    {
        public ApiResponse<ManageBorrowedBooksDto> GetExamples()
        {
            return new ApiResponse<ManageBorrowedBooksDto>(200, "Successfully retrieved borrowed books", new ManageBorrowedBooksDto
            {
                BorrowedBooks = new List<BorrowedBookDto>
            {
                new BorrowedBookDto
                {
                    Id = 1,
                    BookId = 101,
                    BookName = "Clean Code",
                    BorrowedAt = DateTime.UtcNow.AddDays(-10),
                    DueDate = DateTime.UtcNow.AddDays(20),
                    ReturnedAt = null,
                    Status = BorrowedBookStatus.Active,
                    BorrowerId = "user123",
                    BorrowerName = "Alice Johnson",
                    BorrowerPhoneNo = "123456789",
                    ConfirmedById = "admin456",
                    ConfirmedByUser = "Mr. Admin"
                }
            },
                Metadata = new Metadata
                {
                    Pagination = new Pagination
                    {
                        PageIndex = 1,
                        PageSize = 10,
                        TotalRecords = 1,
                        TotalPages = 1
                    }
                }
            });
        }
    }

    public class GetBorrowedBookByIdSuccessExample : IExamplesProvider<ApiResponse<BorrowedBookDto>>
    {
        public ApiResponse<BorrowedBookDto> GetExamples()
        {
            return new ApiResponse<BorrowedBookDto>(200, "Successfully retrieved borrowed book", new BorrowedBookDto
            {
                Id = 1,
                BookId = 101,
                BookName = "The Pragmatic Programmer",
                BorrowedAt = DateTime.UtcNow.AddDays(-5),
                DueDate = DateTime.UtcNow.AddDays(10),
                ReturnedAt = null,
                Status = BorrowedBookStatus.Active,
                BorrowerId = "user001",
                BorrowerName = "John Doe",
                BorrowerPhoneNo = "555-1234",
                ConfirmedById = "admin001",
                ConfirmedByUser = "Admin Jane"
            });
        }
    }

    public class GetBorrowedBookByIdNotFoundExample : IExamplesProvider<ApiResponse<string>>
    {
        public ApiResponse<string> GetExamples()
        {
            return new ApiResponse<string>(404, "Borrowed book not found");
        }
    }

    public class GetUserBorrowedBooksSuccessExample : IExamplesProvider<ApiResponse<List<BorrowedBookDto>>>
    {
        public ApiResponse<List<BorrowedBookDto>> GetExamples()
        {
            return new ApiResponse<List<BorrowedBookDto>>(200, "Successfully retrieved borrowed books", new List<BorrowedBookDto>
        {
            new BorrowedBookDto
            {
                Id = 1,
                BookId = 101,
                BookName = "Refactoring",
                BorrowedAt = DateTime.UtcNow.AddDays(-3),
                DueDate = DateTime.UtcNow.AddDays(11),
                ReturnedAt = null,
                Status = BorrowedBookStatus.Active,
                BorrowerId = "userXYZ",
                BorrowerName = "Sophia Lee",
                BorrowerPhoneNo = "555-7890",
                ConfirmedById = "admin123",
                ConfirmedByUser = "Admin Max"
            }
        });
        }
    }

    public class GetUserBorrowedBooksNotFoundExample : IExamplesProvider<ApiResponse<string>>
    {
        public ApiResponse<string> GetExamples()
        {
            return new ApiResponse<string>(404, "No borrowed books found for the user");
        }
    }

    public class UpdateBorrowedBookStatusSuccessExample : IExamplesProvider<ApiResponse<string>>
    {
        public ApiResponse<string> GetExamples()
        {
            return new ApiResponse<string>(200, "Status updated successfully");
        }
    }

    public class UpdateBorrowedBookStatusErrorExample : IExamplesProvider<ApiResponse<List<string>>>
    {
        public ApiResponse<List<string>> GetExamples()
        {
            return new ApiResponse<List<string>>(400, "Failed to update borrowed book status", new List<string>
        {
            "Request not found!",
            "Status change not permitted for current state."
        });
        }
    }

}
