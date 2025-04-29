using Readify.BLL.Features.BorrowRequest.DTOs;
using Readify.BLL.Helpers;
using Readify.BLL.Specifications.BorrowRequestSpec;
using Readify.DAL.UOW;

namespace Readify.BLL.Features.BorrowRequest.Services
{
    internal class BorrowRequestService : IBorrowRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IBorrowRequestValidator _borrowRequestValidator;
        private readonly IBorrowedBookService _borrowedBookService;
        private readonly IBookService _bookService;

        public BorrowRequestService(IUnitOfWork unitOfWork, ITokenService tokenService, IBorrowRequestValidator borrowRequestValidator, IBorrowedBookService borrowedBookService, IBookService bookService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _borrowRequestValidator = borrowRequestValidator;
            _borrowedBookService = borrowedBookService;
            _bookService = bookService;
        }
        public async Task<List<string>> CreateBorrowRequestAsync(CreateBorrowRequestDto createBorrowRequestDto)
        {
            List<string> errors = new List<string>();

            var userId = _tokenService.GetUserIdFromToken();
            errors.AddRange(await _borrowRequestValidator.ValidateCreateBorrowRequest(userId, createBorrowRequestDto.BookId, createBorrowRequestDto.StartDate, createBorrowRequestDto.EndDate));

            if (errors.Any())
                return errors;


            var borrowRequest = new DAL.Entities.BorrowRequest
            {
                BookId = createBorrowRequestDto.BookId,
                UserId = userId,
                StartDate = createBorrowRequestDto.StartDate,
                EndDate = createBorrowRequestDto.EndDate,
                RequestedAt = DateTime.UtcNow,
                Status = BorrowRequestStatus.Pending,
                ApprovedBy = null
            };

            _unitOfWork.BorrowRequestRepository.AddEntity(borrowRequest);

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
            errors.AddRange(await _borrowRequestValidator.ValidateDeleteBorrowRequest(id));

            if (errors.Any())
            {
                return errors;
            }

            if (_unitOfWork.BorrowRequestRepository.DeleteEntity(id))
            {
                await _unitOfWork.SaveAsync();
                return errors;
            }

            errors.Add("Borrow Request does not exist!");
            return errors;
        }

        public async Task<ListAllRequestsDto> GetAllBorrowRequestsAsync(BorrowRequestSpecification specs)
        {
            var totalCountSpec = new BorrowRequestSpecificationImpl(specs);
            totalCountSpec.IgnorePagination();

            int matchedFilterationCount = await _unitOfWork.BorrowRequestRepository.CountWithSpecAsync(totalCountSpec);
            int totalCount = await _unitOfWork.BorrowRequestRepository.CountAsync();

            var specifications = new BorrowRequestSpecificationImpl(specs);

            var requests = await _unitOfWork.BorrowRequestRepository.GetWithSpecificationsAsync(specifications)
               ?? Enumerable.Empty<DAL.Entities.BorrowRequest>();

            var borrowRequestDto = requests.Select(br => new BorrowRequestDto
            {
                Id = br.Id,
                BookTitle = br.Book.Title,
                AvailableCopies = br.Book.AvailableCount,
                RequestedBy = br.User.UserName,
                PhoneNumber = br.User.PhoneNumber,
                ApprovedBy = br.ApprovedBy,
                StartDate = br.StartDate,
                EndDate = br.EndDate,
                RequestedAt = br.RequestedAt,
                Status = br.Status,
            });

            var pagination = new Pagination
            {
                PageIndex = specs.PageIndex,
                PageSize = specs.PageSize,
                TotalRecords = matchedFilterationCount,
                TotalPages = (int)Math.Ceiling((double)matchedFilterationCount / specs.PageSize)
            };

            return new ListAllRequestsDto
            {
                BorrowRequests = borrowRequestDto.ToList(),
                Metadata = new Metadata()
                {
                    Pagination = pagination,
                }
            };

        }
        public async Task<BorrowRequestDto> GetBorrowRequestByIdAsync(int id)
        {
            var borrowRequest = await _unitOfWork.BorrowRequestRepository.GetRequestById(id);

            if (borrowRequest == null)
                return null;

            return new BorrowRequestDto
            {
                Id = borrowRequest.Id,
                BookTitle = borrowRequest.Book.Title,
                AvailableCopies = borrowRequest.Book.AvailableCount,
                RequestedBy = borrowRequest.User.UserName,
                PhoneNumber = borrowRequest.User.PhoneNumber,
                ApprovedBy = borrowRequest.ApprovedBy,
                StartDate = borrowRequest.StartDate,
                EndDate = borrowRequest.EndDate,
                RequestedAt = borrowRequest.RequestedAt,
                Status = borrowRequest.Status,
            };
        }

        public async Task<List<BorrowRequestDto>> GetUserBorrowRequestsAsync()
        {
            var userId = _tokenService.GetUserIdFromToken();
            var userRequests = await _unitOfWork.BorrowRequestRepository.GetUserRequestsIncludesAsync(userId);

            if (userRequests == null)
                return null;

            return userRequests.Select(br => new BorrowRequestDto
            {
                Id = br.Id,
                BookTitle = br.Book.Title,
                AvailableCopies = br.Book.AvailableCount,
                RequestedBy = br.User.UserName,
                PhoneNumber = br.User.PhoneNumber,
                ApprovedBy = br.ApprovedBy,
                StartDate = br.StartDate,
                EndDate = br.EndDate,
                RequestedAt = br.RequestedAt,
                Status = br.Status,
            }).ToList();
        }

        public async Task<List<string>> UpdateBorrowRequestStatusAsync(UpdateBorrowRequestStatusDto requestDto)
        {
            List<string> errors = new List<string>();

            var request = await _unitOfWork.BorrowRequestRepository.GetRequestById(requestDto.Id);

            if (request == null)
            {
                errors.Add("Request not found!");
                return errors;
            }

            errors.AddRange(await _borrowRequestValidator.ValidateUpdateBorrowRequest(requestDto.Id, requestDto.Status));

            if (errors.Any())
            {
                return errors;
            }

            request.Status = requestDto.Status;
            var user = await _tokenService.GetUserFromTokenAsync();
            request.ApprovedBy = user.Fullname;

            _unitOfWork.BorrowRequestRepository.UpdateEntity(request);
            int affectedRows = await _unitOfWork.SaveAsync();

            if (affectedRows <= 0)
            {
                errors.Add("An error occurred while updating the status.");
                return errors;
            }


            if (request.Status == BorrowRequestStatus.Approved)
            {
                errors.AddRange(await _borrowedBookService.AddBorrowedBookAsync(request));
                errors.AddRange(await _bookService.DecrementAvailableCopies(request.BookId));
            }

            return errors;
        }
    }
}
