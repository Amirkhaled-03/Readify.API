using Readify.BLL.Features.BorrowRequest.DTOs;
using Readify.BLL.Specifications.BorrowRequestSpec;
using Readify.BLL.Specifications.UserBorrowRequestSpec;

namespace Readify.BLL.Features.BorrowRequest.Services
{
    public interface IBorrowRequestService
    {
        Task<List<string>> CreateBorrowRequestAsync(CreateBorrowRequestDto createBorrowRequestDto);
        Task<List<string>> DeleteRequestByIdAsync(int id);
        Task<ListAllRequestsDto> GetAllBorrowRequestsAsync(BorrowRequestSpecification specs);
        Task<BorrowRequestDto> GetBorrowRequestByIdAsync(int id);
        Task<ListAllRequestsDto> GetUserBorrowRequestsAsync(UserBorrowRequestSpecification specs);
        Task<List<string>> UpdateBorrowRequestStatusAsync(UpdateBorrowRequestStatusDto requestDto);

    }
}
