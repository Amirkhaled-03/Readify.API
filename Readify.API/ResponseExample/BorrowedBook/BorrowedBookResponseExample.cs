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

    public class GetRecommendedBooksSuccessExample : IExamplesProvider<ApiResponse<RecommendedBooksDto>>
    {
        public ApiResponse<RecommendedBooksDto> GetExamples()
        {
            return new ApiResponse<RecommendedBooksDto>(200, "Success", new RecommendedBooksDto
            {
                LastBorrowedBookName = "Atomic Habits",
                IsBorrowing = 1,
                Books = new List<BookDto>
            {
                new BookDto
                {
                    Id = 2,
                    Title = "Deep Work",
                    Author = "Cal Newport",
                    ISBN = "9781455586691",
                    Description = "Rules for focused success in a distracted world.",
                    Language = "English",
                    PageCount = 304,
                    Price = 18.99M,
                    PublishYear = 2016,
                    Rating = 4.4,
                    AvailableCount = 3,
                    CreatedAt = DateTime.UtcNow.AddMonths(-6),
                    Categories = new List<string> { "Self-help", "Productivity" },
                    Image = null
                }
            }
            });
        }
    }

}
