using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Readify.BLL.Features.JWTToken;
using Readify.DAL.UOW;

namespace Readify.BLL.Validators.BorrowedBookValidators
{
    internal class BorrowedBookValidator : IBorrowedBookValidator
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        public BorrowedBookValidator(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<List<string>> ValidateUpdateBorrowedBook(int id, BorrowedBookStatus status)
        {
            List<string> errors = new List<string>();

            var borrowedBook = await _unitOfWork.BorrowedBookRepository.GetFirstOrDefaultAsync(bb => bb.Id == id);

            if (borrowedBook == null)
            {
                errors.Add("Borrowed book not found.");
                return errors;
            }

            switch (status)
            {
                case BorrowedBookStatus.Active:
                    if (borrowedBook.Status == BorrowedBookStatus.Active)
                        errors.Add("Book is already marked as active.");
                    break;

                case BorrowedBookStatus.Returned:
                    if (borrowedBook.ReturnedAt == null)
                    {
                        // Good: allow transition to returned
                    }
                    else
                    {
                        errors.Add("Book is already marked as returned.");
                    }
                    break;

                case BorrowedBookStatus.Overdue:
                    if (borrowedBook.DueDate >= DateTime.UtcNow)
                    {
                        errors.Add("Cannot mark as overdue before due date.");
                    }
                    break;

                default:
                    errors.Add("Invalid status.");
                    break;
            }
            return errors;
        }
    }
}
