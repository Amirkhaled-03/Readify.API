using Readify.API.HandleResponses;
using Readify.BLL.Features.BorrowRequest.DTOs;
using Readify.BLL.Helpers;
using Swashbuckle.AspNetCore.Filters;

namespace Readify.API.ResponseExample.BorrowRequest
{
    public class GetAllBorrowRequestsSuccessExample : IExamplesProvider<ApiResponse<ListAllRequestsDto>>
    {
        public ApiResponse<ListAllRequestsDto> GetExamples()
        {
            return new ApiResponse<ListAllRequestsDto>(200, "Borrow requests retrieved successfully", new ListAllRequestsDto
            {
                BorrowRequests = new List<BorrowRequestDto>
            {
                new BorrowRequestDto
                {
                    Id = 1,
                    BookTitle = "The Great Gatsby",
                    AvailableCopies = 3,
                    RequestedBy = "john_doe",
                    PhoneNumber = "+1234567890",
                    ApprovedBy = "admin",
                    StartDate = DateTime.UtcNow.AddDays(1),
                    EndDate = DateTime.UtcNow.AddDays(7),
                    RequestedAt = DateTime.UtcNow,
                    Status = BorrowRequestStatus.Pending
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

    public class GetAllBorrowRequestsErrorExample : IExamplesProvider<ApiResponse<List<string>>>
    {
        public ApiResponse<List<string>> GetExamples()
        {
            return new ApiResponse<List<string>>(400, "Bad request due to invalid parameters", new List<string>
        {
            "Invalid date range specified.",
            "Page index out of range."
        });
        }
    }

    public class GetBorrowRequestByIdSuccessExample : IExamplesProvider<ApiResponse<BorrowRequestDto>>
    {
        public ApiResponse<BorrowRequestDto> GetExamples()
        {
            return new ApiResponse<BorrowRequestDto>(200, "Borrow request retrieved successfully", new BorrowRequestDto
            {
                Id = 1,
                BookTitle = "The Great Gatsby",
                AvailableCopies = 3,
                RequestedBy = "john_doe",
                PhoneNumber = "+1234567890",
                ApprovedBy = "admin",
                StartDate = DateTime.UtcNow.AddDays(1),
                EndDate = DateTime.UtcNow.AddDays(7),
                RequestedAt = DateTime.UtcNow,
                Status = BorrowRequestStatus.Pending,
            });
        }
    }

    public class GetBorrowRequestByIdErrorExample : IExamplesProvider<ApiResponse<string>>
    {
        public ApiResponse<string> GetExamples()
        {
            return new ApiResponse<string>(404, "Borrow request not found", "The borrow request with the specified ID does not exist.");
        }
    }

    public class GetUserBorrowRequestsSuccessExample : IExamplesProvider<ApiResponse<ListAllRequestsDto>>
    {
        public ApiResponse<ListAllRequestsDto> GetExamples()
        {
            return new ApiResponse<ListAllRequestsDto>(200, "Borrow requests retrieved successfully", new ListAllRequestsDto
            {
                BorrowRequests = new List<BorrowRequestDto>
            {
                new BorrowRequestDto
                {
                    Id = 1,
                    BookTitle = "Clean Code",
                    AvailableCopies = 2,
                    RequestedBy = "student01",
                    PhoneNumber = "555-1234",
                    ApprovedBy = "Librarian Mary",
                    StartDate = DateTime.UtcNow.AddDays(2),
                    EndDate = DateTime.UtcNow.AddDays(12),
                    RequestedAt = DateTime.UtcNow,
                    Status = BorrowRequestStatus.Pending
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

    public class GetUserBorrowRequestsErrorExample : IExamplesProvider<ApiResponse<string>>
    {
        public ApiResponse<string> GetExamples()
        {
            return new ApiResponse<string>(404, "No borrow requests found for user", "The user does not have any borrow requests.");
        }
    }

    public class CreateBorrowRequestSuccessExample : IExamplesProvider<ApiResponse<string>>
    {
        public ApiResponse<string> GetExamples()
        {
            return new ApiResponse<string>(200, "Borrow request created successfully", "Request has been created.");
        }
    }

    public class CreateBorrowRequestErrorExample : IExamplesProvider<ApiResponse<List<string>>>
    {
        public ApiResponse<List<string>> GetExamples()
        {
            return new ApiResponse<List<string>>(400, "Invalid input or failed validation", new List<string>
        {
            "Book ID is invalid.",
            "Start date is not within the allowed range.",
            "End date is earlier than start date."
        });
        }
    }

    public class DeleteBorrowRequestSuccessExample : IExamplesProvider<ApiResponse<string>>
    {
        public ApiResponse<string> GetExamples()
        {
            return new ApiResponse<string>(200, "Borrow request deleted successfully", "Request has been deleted.");
        }
    }

    public class DeleteBorrowRequestErrorExample : IExamplesProvider<ApiResponse<List<string>>>
    {
        public ApiResponse<List<string>> GetExamples()
        {
            return new ApiResponse<List<string>>(400, "Failed validation or request not found", new List<string>
        {
            "Borrow request not found with the provided ID."
        });
        }
    }

    public class UpdateBorrowRequestStatusSuccessExample : IExamplesProvider<ApiResponse<string>>
    {
        public ApiResponse<string> GetExamples()
        {
            return new ApiResponse<string>(200, "Borrow request status updated successfully", "Request status has been updated.");
        }
    }

    public class UpdateBorrowRequestStatusErrorExample : IExamplesProvider<ApiResponse<List<string>>>
    {
        public ApiResponse<List<string>> GetExamples()
        {
            return new ApiResponse<List<string>>(400, "Failed validation or request not found", new List<string>
        {
            "Request not found!",
            "Invalid status transition."
        });
        }
    }

}
