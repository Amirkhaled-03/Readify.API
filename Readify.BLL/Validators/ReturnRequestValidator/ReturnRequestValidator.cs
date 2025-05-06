using Readify.DAL.UOW;

namespace Readify.BLL.Validators.ReturnRequestValidator
{
    internal class ReturnRequestValidator : IReturnRequestValidator
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        public ReturnRequestValidator(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<List<string>> ValidateCreateReturnRequest(int borrowedBookId, DateTime returnDate)
        {
            List<string> errors = new List<string>();

            var borrowedBook = await _unitOfWork.BorrowedBookRepository.GetBorrowedBookByIdAsync(borrowedBookId);
            if (borrowedBook == null)
            {
                errors.Add("Borrowed Record does not exist!");
                return errors;
            }
            var userId = _tokenService.GetUserIdFromToken();
            if(borrowedBook.UserId != userId)
            {
                errors.Add("Request can only be made by borrowed record user!");
                return errors;
            }

            if (borrowedBook.Status == BorrowedBookStatus.Returned)
            {
                errors.Add("This book has already been returned.");
            }

            if (_unitOfWork.ReturnRequestRepository == null)
                throw new Exception("ReturnRequestRepository is null");

            var existingReturnRequest = await _unitOfWork.ReturnRequestRepository.GetFirstOrDefaultAsync(rr =>
            rr.BorrowedBookId == borrowedBookId);
            if (existingReturnRequest != null &&
            existingReturnRequest.Status != ReturnRequestStatus.Rejected)
            {
                errors.Add("A return request for this borrowed book already exists.");
            }

            if (returnDate < DateTime.UtcNow)
            {
                errors.Add("Return date cannot be in the past.");
            }

            if (returnDate < borrowedBook.BorrowedAt)
            {
                errors.Add("Return date cannot be before the book was borrowed.");
            }

            return errors;
        }

        public async Task<List<string>> ValidateDeleteReturnRequest(int id)
        {
            List<string> errors = new List<string>();

            var returnRequest = await _unitOfWork.ReturnRequestRepository.GetFirstOrDefaultAsync(rr => rr.Id == id);

            if (returnRequest == null)
            {
                errors.Add("Return request does not exist!");
                return errors;
            }

            if (returnRequest.Status != ReturnRequestStatus.Pending)
            {
                errors.Add("Can only delete borrow requests that are pending");
            }

            return errors;
        }

        public async Task<List<string>> ValidateUpdateReturnRequest(int id, ReturnRequestStatus status)
        {
            var errors = new List<string>();

            var currentUserId = _tokenService.GetUserIdFromToken();

            var returnRequest = await _unitOfWork.ReturnRequestRepository.GetByIdAsync(id);

            if (returnRequest == null)
            {
                errors.Add("Return request not found.");
                return errors;
            }

            if (returnRequest.BorrowedBook.UserId == _tokenService.GetUserIdFromToken())
            {
                errors.Add("Request cannot be changed by user who made it!");
                return errors;
            }

            if (returnRequest.BorrowedBook == null)
            {
                errors.Add("Borrowed book information is missing.");
                return errors;
            }

            if (returnRequest.Status != ReturnRequestStatus.Pending)
                errors.Add("Can only change pending requests!");

            if (returnRequest.Status == status)
                errors.Add("Status is already set");

            return errors;
        }
    }
}
