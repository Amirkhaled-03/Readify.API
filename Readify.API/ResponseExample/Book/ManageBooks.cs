using Readify.API.HandleResponses;
using Readify.BLL.Features.Book.DTOs;
using Readify.BLL.Helpers;
using Swashbuckle.AspNetCore.Filters;

namespace Readify.API.ResponseExample.Book
{
    public class ManageBooksPageExample : IExamplesProvider<ApiResponse<ManageBooksPageDto>>
    {
        public ApiResponse<ManageBooksPageDto> GetExamples()
        {
            return new ApiResponse<ManageBooksPageDto>(200, "success", data: new ManageBooksPageDto
            {
                Books = new List<BookDto>
            {
                new BookDto
                {
                    Id = 1,
                    Title = "Clean Code",
                    Author = "Robert C. Martin",
                    ISBN = "9780132350884",
                    Description = "A handbook of agile software craftsmanship.",
                    PublishYear = 2008,
                    Language = "English",
                    PageCount = 464,
                    Price = 39.99m,
                    Rating = 4.8,
                    AvailableCount = 5,
                    CreatedAt = DateTime.UtcNow.AddDays(-10)
                },
                new BookDto
                {
                    Id = 2,
                    Title = "The Pragmatic Programmer",
                    Author = "Andrew Hunt",
                    ISBN = "9780201616224",
                    Description = "Classic tips and best practices for software developers.",
                    PublishYear = 2008,
                    Language = "English",
                    PageCount = 464,
                    Price = 39.99m,
                    Rating = 4.8,
                    AvailableCount = 3,
                    CreatedAt = DateTime.UtcNow.AddDays(-20)
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

    public class GetBookDetailsSuccessExample : IExamplesProvider<ApiResponse<BookDetailsDto>>
    {
        public ApiResponse<BookDetailsDto> GetExamples()
        {
            return new ApiResponse<BookDetailsDto>(200, "success", new BookDetailsDto
            {
                Id = 1,
                Title = "Clean Code",
                Author = "Robert C. Martin",
                ISBN = "9780132350884",
                Description = "A handbook of agile software craftsmanship.",
                PublishYear = 2008,
                Language = "English",
                PageCount = 464,
                Price = 39.99m,
                Rating = 4.8,
                AvailableCount = 5,
                CreatedBy = "Admin User",
                CreatedAt = DateTime.UtcNow.AddDays(-30),
                Image = null, // base64 string can go here if available
                Categories = new List<string> { "Programming", "Software Engineering" }
            });
        }
    }

    public class GetBookDetailsNotFoundExample : IExamplesProvider<ApiResponse<object>>
    {
        public ApiResponse<object> GetExamples()
        {
            return new ApiResponse<object>(404, "Book not found");
        }
    }

    public class AddBookSuccessExample : IExamplesProvider<ApiResponse<List<string>>>
    {
        public ApiResponse<List<string>> GetExamples()
        {
            return new ApiResponse<List<string>>(201, "Book created successfully", data: new List<string>());
        }
    }

    public class AddBookFailedExample : IExamplesProvider<ApiResponse<List<string>>>
    {
        public ApiResponse<List<string>> GetExamples()
        {
            return new ApiResponse<List<string>>(400, "bad request", errors: new List<string>
            {
                "ISBN already exists.",
                "A book with the same title and author already exists."
            });
        }
    }

    public class UpdateBookSuccessExample : IExamplesProvider<ApiResponse<List<string>>>
    {
        public ApiResponse<List<string>> GetExamples()
        {
            return new ApiResponse<List<string>>(200, "Book updated successfully", data: new List<string>());
        }
    }

    public class UpdateBookFailedExample : IExamplesProvider<ApiResponse<List<string>>>
    {
        public ApiResponse<List<string>> GetExamples()
        {
            return new ApiResponse<List<string>>(400, "bad request", errors: new List<string>
            {
                "This book not exists",
                "ISBN already in use by another book.",
                "An error occurred while updating the Book."
            });
        }
    }

    public class DeleteBookSuccessExample : IExamplesProvider<ApiResponse<List<string>>>
    {
        public ApiResponse<List<string>> GetExamples()
        {
            return new ApiResponse<List<string>>(200, "Book deleted successfully", data: new List<string>());
        }
    }

    public class DeleteBookFailedExample : IExamplesProvider<ApiResponse<List<string>>>
    {
        public ApiResponse<List<string>> GetExamples()
        {
            return new ApiResponse<List<string>>(400, "Failed to delete book", errors: new List<string>
            {
                "The book is currently borrowed and cannot be deleted.",
                "There are active borrow requests for this book. Deletion is not allowed.",
                "This book not exists, cannot delete it."
            });
        }
    }

    public class ChangeBookImageSuccessExample : IExamplesProvider<ApiResponse<List<string>>>
    {
        public ApiResponse<List<string>> GetExamples()
        {
            return new ApiResponse<List<string>>(200, "Book image updated successfully.", data: new List<string>());
        }
    }

    public class ChangeBookImageErrorsExample : IExamplesProvider<ApiResponse<List<string>>>
    {
        public ApiResponse<List<string>> GetExamples()
        {
            return new ApiResponse<List<string>>(400, "Failed to update book image", data: new List<string>
        {
            "Book not found.",
            "No image uploaded.",
            "An error occurred while uploading the image. Please try again.",
            "An error occurred while updating the book image."
        });
        }
    }

    public class UpdateBookCategoriesSuccessExample : IExamplesProvider<ApiResponse<List<string>>>
    {
        public ApiResponse<List<string>> GetExamples()
        {
            return new ApiResponse<List<string>>(200, "Book categories updated successfully.", new List<string>());
        }
    }

    public class UpdateBookCategoriesErrorExample : IExamplesProvider<ApiResponse<List<string>>>
    {
        public ApiResponse<List<string>> GetExamples()
        {
            return new ApiResponse<List<string>>(400, "Failed to update book categories", new List<string>
        {
            "book not found.",
            "Failed to assign new categories to the book."
        });
        }
    }

    public class GetBooksByCategorySuccessExample : IExamplesProvider<ApiResponse<List<BookDto>>>
    {
        public ApiResponse<List<BookDto>> GetExamples()
        {
            return new ApiResponse<List<BookDto>>(200, "Success", new List<BookDto>
        {
            new BookDto
            {
                Id = 10,
                Title = "Domain-Driven Design",
                Author = "Eric Evans",
                ISBN = "9780321125217",
                AvailableCount = 2,
                Description = "Tackling complexity in the heart of software.",
                Categories = new List<string> { "Software Engineering" }
            }
        });
        }
    }

    public class GetBooksByCategoryNotFoundExample : IExamplesProvider<ApiResponse<string>>
    {
        public ApiResponse<string> GetExamples()
        {
            return new ApiResponse<string>(404, "No books found in this category.");
        }
    }
}