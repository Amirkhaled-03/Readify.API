using Readify.API.HandleResponses;
using Readify.BLL.Features.Book.DTOs;
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

    public class GetUserBorrowedBooksSuccessExample : IExamplesProvider<ApiResponse<ManageBorrowedBooksDto>>
    {
        public ApiResponse<ManageBorrowedBooksDto> GetExamples()
        {
            return new ApiResponse<ManageBorrowedBooksDto>(200, "Borrowed books retrieved successfully", new ManageBorrowedBooksDto
            {
                BorrowedBooks = new List<BorrowedBookDto>
            {
                new BorrowedBookDto
                {
                    Id = 1,
                    BookId = 101,
                    BookName = "The Pragmatic Programmer",
                    Author = "Andrew Hunt",
                    BorrowedAt = DateTime.UtcNow.AddDays(-15),
                    DueDate = DateTime.UtcNow.AddDays(15),
                    ReturnedAt = null,
                    BorrowerId = "user-456",
                    BorrowerName = "John Doe",
                    BorrowerPhoneNo = "123-456-7890",
                    ConfirmedById = "admin-001",
                    ConfirmedByUser = "Librarian Smith",
                    Status = BorrowedBookStatus.Active
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

    public class GetRecommendedBooksSuccessExample : IExamplesProvider<ApiResponse<List<BookDto>>>
    {
        public ApiResponse<List<BookDto>> GetExamples()
        {
            return new ApiResponse<List<BookDto>>(200, "Success", new List<BookDto>
        {
            new BookDto
            {
                Id = 1,
                Title = "Clean Code",
                Author = "Robert C. Martin",
                ISBN = "9780132350884",
                AvailableCount = 3,
                Description = "A handbook of agile software craftsmanship."
                // Add other fields if needed
            },
            new BookDto
            {
                Id = 2,
                Title = "The Pragmatic Programmer",
                Author = "Andy Hunt",
                ISBN = "9780201616224",
                AvailableCount = 5,
                Description = "Journey to mastery in software development."
            }
        });
        }
    }

    public class GetRecommendedBooksNotFoundExample : IExamplesProvider<ApiResponse<string>>
    {
        public ApiResponse<string> GetExamples()
        {
            return new ApiResponse<string>(404, "No recommended books found.");
        }
    }

}
