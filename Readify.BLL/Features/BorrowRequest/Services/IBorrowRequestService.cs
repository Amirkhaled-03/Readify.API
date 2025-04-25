using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Readify.BLL.Features.BorrowRequest.DTOs;
using Readify.BLL.Specifications.BorrowRequestSpec;

namespace Readify.BLL.Features.BorrowRequest.Services
{
    public interface IBorrowRequestService
    {
        Task<List<string>> CreateBorrowRequestAsync(CreateBorrowRequestDto createBorrowRequestDto);
        Task<List<string>> DeleteRequestByIdAsync(int id);
        Task<List<BorrowRequestDto>> GetAllBorrowRequestsAsync(BorrowRequestSpecification specs);
        Task<BorrowRequestDto> GetBorrowRequestByIdAsync(int id);
        Task<List<BorrowRequestDto>> GetUserBorrowRequestsAsync();
        Task<List<string>> UpdateBorrowRequestStatusAsync(UpdateBorrowRequestStatusDto requestDto);

    }
}
