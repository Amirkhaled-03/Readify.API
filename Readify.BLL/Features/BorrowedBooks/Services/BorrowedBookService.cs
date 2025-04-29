using Readify.BLL.Features.BorrowedBooks.DTOs;
using Readify.BLL.Helpers;
using Readify.BLL.Specifications.BorrowedBookSpec;
using Readify.DAL.Entities;
using Readify.DAL.UOW;

namespace Readify.BLL.Features.BorrowedBooks.Services
{
    internal class BorrowedBookService : IBorrowedBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IBorrowedBookValidator _borrowedBookValidator;
        private readonly IBookService _bookService;
        public BorrowedBookService(IUnitOfWork unitOfWork, ITokenService tokenService, IBorrowedBookValidator borrowedBookValidator, IBookService bookService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _borrowedBookValidator = borrowedBookValidator;
            _bookService = bookService;
        }

        public async Task<ManageBorrowedBooksDto> GetAllBorrowedBooksAsync(BorrowedBookSpecification specs)
        {
            await UpdateOverdueBooksAsync();

            var totalCountSpec = new BorrowedBookSpecificationImpl(specs);
            totalCountSpec.IgnorePagination();

            int matchedFilterationCount = await _unitOfWork.BorrowedBookRepository.CountWithSpecAsync(totalCountSpec);
            int totalCount = await _unitOfWork.BorrowedBookRepository.CountAsync();

            var specifications = new BorrowedBookSpecificationImpl(specs);

            var borrowedBooks = await _unitOfWork.BorrowedBookRepository.GetWithSpecificationsAsync(specifications)
               ?? Enumerable.Empty<DAL.Entities.BorrowedBook>();

            var borrowedBookDto = borrowedBooks.Select(bb => new BorrowedBookDto
            {
                Id = bb.Id,
                BookId = bb.BookId,
                BorrowedAt = bb.BorrowedAt,
                ConfirmedById = bb.ConfirmedBy == null ? null : bb.ConfirmedBy,
                DueDate = bb.DueDate,
                ReturnedAt = bb.ReturnedAt,
                BorrowerId = bb.UserId,
                BookName = bb.Book.Title,
                BorrowerName = bb.User.Fullname,
                BorrowerPhoneNo = bb.User.PhoneNumber,
                ConfirmedByUser = bb.ConfirmedBy == null ? null : bb.ConfirmedBy,
                Status = bb.Status,
            });

            var pagination = new Pagination
            {
                PageIndex = specs.PageIndex,
                PageSize = specs.PageSize,
                TotalRecords = matchedFilterationCount,
                TotalPages = (int)Math.Ceiling((double)matchedFilterationCount / specs.PageSize)
            };

            return new ManageBorrowedBooksDto
            {
                BorrowedBooks = borrowedBookDto.ToList(),
                Metadata = new Metadata()
                {
                    Pagination = pagination,
                }
            };
        }

        public async Task<BorrowedBookDto> GetBorrowedBookByIdAsync(int id)
        {
            await UpdateOverdueBooksAsync();

            var book = await _unitOfWork.BorrowedBookRepository.GetBorrowedBookByIdAsync(id);

            if (book == null)
                return null;

            return new BorrowedBookDto
            {
                Id = book.Id,
                BorrowerId = book.UserId,
                BookName = book.Book.Title,
                BookId = book.Id,
                BorrowedAt = book.BorrowedAt,
                BorrowerName = book.User.Fullname,
                BorrowerPhoneNo = book.User.PhoneNumber,
                ConfirmedById = book.ConfirmedBy,
                ConfirmedByUser = book.ConfirmedBy == null ? null : book.ConfirmedBy,
                DueDate = book.DueDate,
                ReturnedAt = book.ReturnedAt,
                Status = book.Status
            };
        }


        public async Task<List<string>> AddBorrowedBookAsync(DAL.Entities.BorrowRequest bookDto)
        {
            List<string> errors = new List<string>();

            var book = new BorrowedBook
            {
                BookId = bookDto.BookId,
                UserId = bookDto.UserId,
                BorrowedAt = bookDto.StartDate,
                DueDate = bookDto.EndDate,
                Status = BorrowedBookStatus.Active,
            };

            _unitOfWork.BorrowedBookRepository.AddEntity(book);
            int bookaffectedRows = await _unitOfWork.SaveAsync();

            if (bookaffectedRows <= 0)
                errors.Add("An error occurred while adding borrowed book.");

            return errors;

        }

        public async Task<List<BorrowedBookDto>> GetUserBorrowBooksAsync()
        {
            await UpdateOverdueBooksAsync();

            var userId = _tokenService.GetUserIdFromToken();
            var userBorrowedBooks = await _unitOfWork.BorrowedBookRepository.GetUserBorrowedBooksAsync(userId);

            if (userBorrowedBooks == null)
                return null;

            return userBorrowedBooks.Select(br => new BorrowedBookDto
            {
                Id = br.Id,
                BorrowerId = br.UserId,
                BookName = br.Book.Title,
                BookId = br.Id,
                BorrowedAt = br.BorrowedAt,
                BorrowerName = br.User.Fullname,
                BorrowerPhoneNo = br.User.PhoneNumber,
                ConfirmedById = br.ConfirmedBy,
                ConfirmedByUser = br.ConfirmedBy,
                DueDate = br.DueDate,
                ReturnedAt = br.ReturnedAt,
                Status = br.Status
            }).ToList();
        }

        public async Task<List<string>> UpdateBorrowedBookStatusAsync(UpdateBorrowedBookStatusDto bookDto)
        {
            List<string> errors = new List<string>();

            var borrowedbook = await _unitOfWork.BorrowedBookRepository.GetFirstOrDefaultAsync(bb => bb.Id == bookDto.Id);

            if (borrowedbook == null)
            {
                errors.Add("Request not found!");
                return errors;
            }

            errors.AddRange(await _borrowedBookValidator.ValidateUpdateBorrowedBook(bookDto.Id, bookDto.Status));

            if (errors.Any())
            {
                return errors;
            }

            borrowedbook.Status = bookDto.Status;
            if (bookDto.Status == BorrowedBookStatus.Returned)
            {
                borrowedbook.ConfirmedBy = _tokenService.GetUserIdFromToken();
                borrowedbook.ReturnedAt = DateTime.UtcNow;
            }

            _unitOfWork.BorrowedBookRepository.UpdateEntity(borrowedbook);
            int affectedRows = await _unitOfWork.SaveAsync();

            if (affectedRows <= 0)
            {
                errors.Add("An error occurred while updating the status.");
                return errors;
            }


            if (borrowedbook.Status == BorrowedBookStatus.Returned)
            {
                errors.AddRange(await _bookService.IncrementAvailableCopies(borrowedbook.BookId));
            }

            return errors;
        }

        private async Task UpdateOverdueBooksAsync()
        {
            var borrowedBooks = await _unitOfWork.BorrowedBookRepository.GetAllAsync();

            foreach (var book in borrowedBooks)
            {
                if (book.Status == BorrowedBookStatus.Active && book.DueDate < DateTime.UtcNow)
                {
                    book.Status = BorrowedBookStatus.Overdue;
                    _unitOfWork.BorrowedBookRepository.UpdateEntity(book);
                }
            }

            await _unitOfWork.SaveAsync();
        }

    }
}
