using Readify.DAL.UOW;

namespace Readify.BLL.Validators.BookValidator
{
    public class BookValidator : IBookValidator
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<string>> ValidateAddBook(string isbn, string title, string authorName)
        {
            List<string> errors = new List<string>();

            // Check if ISBN is unique
            var existingBook = await _unitOfWork.BookRepository.GetSpecificColumnFirstOrDefaultAsync(b => b.ISBN == isbn);
            if (existingBook)
                errors.Add("This ISBN already exists.");

            // Check if the title is unique for the given author
            existingBook = await _unitOfWork.BookRepository.GetSpecificColumnFirstOrDefaultAsync(b => b.Title == title && b.Author == authorName);
            if (existingBook)
                errors.Add("This title already exists for the given author.");

            return errors;
        }

        public async Task<List<string>> ValidateEditBook(int bookId, string isbn, string title, string authorName)
        {
            List<string> errors = new List<string>();

            // Check if ISBN is unique (exclude the current book being edited by its bookId)
            var existingBook = await _unitOfWork.BookRepository.GetSpecificColumnFirstOrDefaultAsync(b => b.Id != bookId && b.ISBN == isbn);
            if (existingBook)
                errors.Add("This ISBN already exists.");

            // Check if the title is unique for the given author (exclude the current book being edited)
            existingBook = await _unitOfWork.BookRepository.GetSpecificColumnFirstOrDefaultAsync(b => b.Id != bookId && b.Title == title && b.Author == authorName);
            if (existingBook)
                errors.Add("This title already exists for the given author.");

            return errors;
        }

        public async Task<List<string>> ValidateDeleteBook(int bookId)
        {
            List<string> errors = new List<string>();

            // Check if the book is currently borrowed and not returned
            var isBorrowed = await _unitOfWork.BorrowedBookRepository
                .GetSpecificColumnFirstOrDefaultAsync(b => b.BookId == bookId && b.ReturnedAt == null);

            if (isBorrowed)
                errors.Add("The book is currently borrowed and cannot be deleted.");

            // Check if there are ANY borrow requests associated with the book
            var hasBorrowRequests = await _unitOfWork.BorrowRequestRepository
                .GetSpecificColumnFirstOrDefaultAsync(r => r.BookId == bookId);

            if (hasBorrowRequests)
                errors.Add("There are borrow requests associated with this book. Deletion is not allowed.");

            return errors;
        }


    }
}