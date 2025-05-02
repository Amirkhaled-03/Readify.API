using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Readify.BLL.Features.BorrowRequest.DTOs;
using Readify.BLL.Features.ReturnRequest.DTOs;
using Readify.BLL.Specifications.BorrowRequestSpec;
using Readify.BLL.Specifications.ReturnRequestSpec;

namespace Readify.BLL.Features.ReturnRequest.Services
{
    public interface IReturnRequestService
    {
        Task<List<string>> CreateReturnRequestAsync(CreateReturnRequestDto createReturnRequestDto);
        Task<List<string>> DeleteRequestByIdAsync(int id);
        Task<ListReturnRequestsDto> GetAllReturnRequestsAsync(ReturnRequestSpecification specs);
        Task<DetailedReturnRequestDto> GetReturnRequestByIdAsync(int id);
        Task<List<string>> UpdateReturnRequestStatusAsync(UpdateReturnRequestDto requestDto);

    }
}
