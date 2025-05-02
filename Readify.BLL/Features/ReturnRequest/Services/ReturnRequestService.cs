using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Readify.BLL.Features.BorrowRequest.DTOs;
using Readify.BLL.Features.ReturnRequest.DTOs;
using Readify.BLL.Helpers;
using Readify.BLL.Specifications.ReturnRequestSpec;
using Readify.BLL.Validators.ReturnRequestValidator;
using Readify.DAL.UOW;

namespace Readify.BLL.Features.ReturnRequest.Services
{
    internal class ReturnRequestService : IReturnRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IReturnRequestValidator _returnRequestValidator;
        private readonly IBorrowedBookService _borrowedBookService;
        private readonly IBookService _bookService;

        public ReturnRequestService(IUnitOfWork unitOfWork, ITokenService tokenService, IReturnRequestValidator returnRequestValidator, IBorrowedBookService borrowedBookService, IBookService bookService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _returnRequestValidator = returnRequestValidator;
            _borrowedBookService = borrowedBookService;
            _bookService = bookService;
        }

        public async Task<List<string>> CreateReturnRequestAsync(CreateReturnRequestDto createReturnRequestDto)
        {
            List<string> errors = new List<string>();

            var userId = _tokenService.GetUserIdFromToken();
            errors.AddRange(await _returnRequestValidator.ValidateCreateReturnRequest(createReturnRequestDto.BorrowedBookId, createReturnRequestDto.ReturnDate));

            if (errors.Any())
                return errors;


            var returnRequest = new DAL.Entities.ReturnRequest
            {
                BorrowedBookId = createReturnRequestDto.BorrowedBookId,
                ReturnDate = createReturnRequestDto.ReturnDate,
                CreatedAt = DateTime.UtcNow,
                Status = ReturnRequestStatus.Pending,
                ApprovedBy = null
            };

            _unitOfWork.ReturnRequestRepository.AddEntity(returnRequest);

            int affectedRows = await _unitOfWork.SaveAsync();
            if (affectedRows <= 0)
            {
                errors.Add("An error occurred while adding the request.");
            }

            return errors;
        }

        public async Task<List<string>> DeleteRequestByIdAsync(int id)
        {
            List<string> errors = new List<string>();
            errors.AddRange(await _returnRequestValidator.ValidateDeleteReturnRequest(id));

            if (errors.Any())
            {
                return errors;
            }

            if (_unitOfWork.ReturnRequestRepository.DeleteEntity(id))
            {
                await _unitOfWork.SaveAsync();
                return errors;
            }

            errors.Add("Return Request does not exist!");
            return errors;
        }

        public async Task<ListReturnRequestsDto> GetAllReturnRequestsAsync(ReturnRequestSpecification specs)
        {
            var totalCountSpec = new ReturnRequestSpecificationImpl(specs);
            totalCountSpec.IgnorePagination();

            int matchedFilterationCount = await _unitOfWork.ReturnRequestRepository.CountWithSpecAsync(totalCountSpec);
            int totalCount = await _unitOfWork.ReturnRequestRepository.CountAsync();

            var specifications = new ReturnRequestSpecificationImpl(specs);

            var requests = await _unitOfWork.ReturnRequestRepository.GetWithSpecificationsAsync(specifications)
               ?? Enumerable.Empty<DAL.Entities.ReturnRequest>();

            var returnRequestDto = requests.Select(rr => new ReturnRequestDto
            {
                Id = rr.Id,
                BorrowedBookId = rr.BorrowedBookId,
                ApprovedBy = rr.ApprovedBy,
                CreatedAt = rr.CreatedAt,
                ReturnDate = rr.ReturnDate,
                Status = rr.Status,
            });

            var pagination = new Pagination
            {
                PageIndex = specs.PageIndex,
                PageSize = specs.PageSize,
                TotalRecords = matchedFilterationCount,
                TotalPages = (int)Math.Ceiling((double)matchedFilterationCount / specs.PageSize)
            };

            return new ListReturnRequestsDto
            {
                ReturnRequests = returnRequestDto.ToList(),
                Metadata = new Metadata()
                {
                    Pagination = pagination,
                }
            };
        }

        public async Task<DetailedReturnRequestDto> GetReturnRequestByIdAsync(int id)
        {
            var returnRequest = await _unitOfWork.ReturnRequestRepository.GetByIdAsync(id);

            if (returnRequest == null)
                return null;

            return new DetailedReturnRequestDto
            {
                Id = returnRequest.Id,
                BorrowedBookId = returnRequest.BorrowedBookId,
                ReturnDate = returnRequest.ReturnDate,
                Status = returnRequest.Status,
                ApprovedBy = returnRequest.ApprovedBy,
                CreatedAt = returnRequest.CreatedAt,
                BorrowedAt = returnRequest.BorrowedBook.BorrowedAt,
                BorrowedBookName = returnRequest.BorrowedBook.Book.Title,
                DueDate = returnRequest.BorrowedBook.DueDate,
                BorrowerId = returnRequest.BorrowedBook.UserId,
                BorrowerName = returnRequest.BorrowedBook.User.Fullname
            };
        }

        public async Task<List<string>> UpdateReturnRequestStatusAsync(UpdateReturnRequestDto requestDto)
        {
            List<string> errors = new List<string>();

            var request = await _unitOfWork.ReturnRequestRepository.GetByIdAsync(requestDto.Id);

            if (request == null)
            {
                errors.Add("Request not found!");
                return errors;
            }

            errors.AddRange(await _returnRequestValidator.ValidateUpdateReturnRequest(requestDto.Id, requestDto.Status));

            if (errors.Any())
            {
                return errors;
            }

            request.Status = requestDto.Status;
            if (requestDto.Status == ReturnRequestStatus.Approved)
            {
                var user = await _tokenService.GetUserFromTokenAsync();
                request.ApprovedBy = user.Fullname;
            }

            _unitOfWork.ReturnRequestRepository.UpdateEntity(request);
            int affectedRows = await _unitOfWork.SaveAsync();

            if (affectedRows <= 0)
            {
                errors.Add("An error occurred while updating the request.");
                return errors;
            }

            return errors;
        }
    }
}
