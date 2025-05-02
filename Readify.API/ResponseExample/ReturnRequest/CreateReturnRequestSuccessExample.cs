using Readify.API.HandleResponses;
using Readify.BLL.Features.ReturnRequest.DTOs;
using Readify.BLL.Helpers;
using Swashbuckle.AspNetCore.Filters;

namespace Readify.API.ResponseExample.ReturnRequest
{
    public class CreateReturnRequestSuccessExample : IExamplesProvider<ApiResponse<string>>
    {
        public ApiResponse<string> GetExamples()
        {
            return new ApiResponse<string>(201, "Return request created successfully");
        }
    }

    public class CreateReturnRequestErrorExample : IExamplesProvider<ApiResponse<List<string>>>
    {
        public ApiResponse<List<string>> GetExamples()
        {
            return new ApiResponse<List<string>>(400, "Failed to create return request", new List<string>
        {
            "Return date must be after the borrowed date.",
            "A return request already exists for this book."
        });
        }
    }

    public class GetAllReturnRequestsSuccessExample : IExamplesProvider<ApiResponse<ListReturnRequestsDto>>
    {
        public ApiResponse<ListReturnRequestsDto> GetExamples()
        {
            return new ApiResponse<ListReturnRequestsDto>(200, "List of return requests retrieved successfully", new ListReturnRequestsDto
            {
                ReturnRequests = new List<ReturnRequestDto>
            {
                new ReturnRequestDto
                {
                    Id = 1,
                    BorrowedBookId = 1001,
                    ApprovedBy = null,
                    CreatedAt = DateTime.UtcNow.AddDays(-2),
                    ReturnDate = DateTime.UtcNow.AddDays(5),
                    Status = ReturnRequestStatus.Pending
                },
                new ReturnRequestDto
                {
                    Id = 2,
                    BorrowedBookId = 1002,
                    ApprovedBy = "admin123",
                    CreatedAt = DateTime.UtcNow.AddDays(-5),
                    ReturnDate = DateTime.UtcNow.AddDays(2),
                    Status = ReturnRequestStatus.Approved
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

    public class GetDetailedReturnRequestSuccessExample : IExamplesProvider<ApiResponse<DetailedReturnRequestDto>>
    {
        public ApiResponse<DetailedReturnRequestDto> GetExamples()
        {
            return new ApiResponse<DetailedReturnRequestDto>(200, "Return request found", new DetailedReturnRequestDto
            {
                Id = 10,
                BorrowedBookId = 105,
                ReturnDate = DateTime.UtcNow.AddDays(2),
                Status = ReturnRequestStatus.Pending,
                ApprovedBy = null,
                CreatedAt = DateTime.UtcNow.AddDays(-3),
                BorrowedAt = DateTime.UtcNow.AddDays(-20),
                BorrowedBookName = "Clean Code",
                DueDate = DateTime.UtcNow.AddDays(-1),
                BorrowerId = "user-123",
                BorrowerName = "Alice Johnson"
            });
        }
    }

    public class GetReturnRequestNotFoundExample : IExamplesProvider<ApiResponse<string>>
    {
        public ApiResponse<string> GetExamples()
        {
            return new ApiResponse<string>(404, "Return request not found");
        }
    }

    public class DeleteReturnRequestSuccessExample : IExamplesProvider<ApiResponse<string>>
    {
        public ApiResponse<string> GetExamples()
        {
            return new ApiResponse<string>(200, "Return request deleted successfully");
        }
    }

    public class DeleteReturnRequestErrorExample : IExamplesProvider<ApiResponse<List<string>>>
    {
        public ApiResponse<List<string>> GetExamples()
        {
            return new ApiResponse<List<string>>(400, "Validation errors occurred", errors: new List<string>
        {
            "Return request cannot be deleted as it has already been approved.",
            "Return request not found."
        });
        }
    }

    public class UpdateReturnRequestSuccessExample : IExamplesProvider<ApiResponse<string>>
    {
        public ApiResponse<string> GetExamples()
        {
            return new ApiResponse<string>(200, "Return request updated successfully");
        }
    }

    public class UpdateReturnRequestErrorExample : IExamplesProvider<ApiResponse<List<string>>>
    {
        public ApiResponse<List<string>> GetExamples()
        {
            return new ApiResponse<List<string>>(400, "Validation or update error", new List<string>
        {
            "Request not found!",
            "Return date cannot be earlier than borrow date."
        });
        }
    }

}
