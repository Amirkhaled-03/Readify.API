using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.Identity.Client;
using Readify.BLL.Features.JWTToken;
using Readify.DAL.UOW;

namespace Readify.BLL.Validators.BorrowRequestValidators
{
    internal class BorrowRequestValidator : IBorrowRequestValidator
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        public BorrowRequestValidator(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<List<string>> ValidateCreateBorrowRequest(string userId, int bookId, DateTime startDate, DateTime endDate)
        {
            List<string> errors = new List<string>();

            var book = await _unitOfWork.BookRepository.GetDetailedBookById(bookId);
            if (book == null)
            {
                errors.Add("Book does not exist!");
                return errors;
            }

            if (book.AvailableCount <= 0)
                errors.Add("No copies available for borrowing!");

            var existingRequest = await _unitOfWork.BorrowRequestRepository
                .GetFirstOrDefaultAsync(br =>
                    br.UserId == userId &&
                    br.BookId == bookId &&
                    (br.Status == BorrowRequestStatus.Pending || br.Status == BorrowRequestStatus.Approved));

            if (existingRequest != null)
                errors.Add("You already have a request for this book!");

            if (startDate < DateTime.UtcNow || endDate < DateTime.UtcNow)
                errors.Add("Date already passed");

            if (endDate < startDate)
                errors.Add("End date must be greater than start date");

            if (endDate > startDate.AddMonths(1))
                errors.Add("End date shouldn't exceed start date by a month");

            return errors;
        }

        public async Task<List<string>> ValidateDeleteBorrowRequest(int id)
        {
            List<string> errors = new List<string>();

            var borrowRequest = await _unitOfWork.BorrowRequestRepository.GetFirstOrDefaultAsync(br => br.Id == id);

            if (borrowRequest == null)
            {
                errors.Add("Borrow request does not exist!");
                return errors;
            }

            var currentUserId = _tokenService.GetUserIdFromToken();

            if (borrowRequest.UserId != currentUserId)
            {
                errors.Add("Request can only be deleted by the user who created it");
            }

            if (borrowRequest.Status != BorrowRequestStatus.Pending)
            {
                errors.Add("Can only delete borrow requests that are pending");
            }

            return errors;
        }

        public async Task<List<string>> ValidateUpdateBorrowRequest(int id, BorrowRequestStatus status)
        {
            List<String> errors = new List<String>();

            var request = await _unitOfWork.BorrowRequestRepository.GetRequestById(id);

            if (request == null)
            {
                errors.Add("Request does not exist");
                return errors;
            }

            if (request.UserId == _tokenService.GetUserIdFromToken())
            {
                errors.Add("Request cannot be accepted by user who made it!");
                return errors;
            }

            if (request.Status != BorrowRequestStatus.Pending)
            {
                errors.Add("Cannot change status of approved/rejected requests");
                return errors;
            }

            if (request.Status == status)
                errors.Add("Status is already set");

            if (status == BorrowRequestStatus.Approved && request.StartDate < DateTime.UtcNow)
                errors.Add("Cannot approve request, start date has already passed!");

            if (status == BorrowRequestStatus.Approved && request.Book.AvailableCount <= 0)
                errors.Add("No available copies of this book");

            return errors;
        }
    }
}
