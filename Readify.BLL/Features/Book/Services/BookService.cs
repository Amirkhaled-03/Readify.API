using Readify.BLL.Constants;
using Readify.BLL.Features.Book.DTOs;
using Readify.BLL.Features.JWTToken;
using Readify.BLL.Helpers;
using Readify.BLL.Specifications.BookSpec;
using Readify.BLL.Validators.BookValidator;
using Readify.DAL.Entities;
using Readify.DAL.UOW;

namespace Readify.BLL.Features.Book.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IBookValidator _bookValidator;

        public BookService(IUnitOfWork unitOfWork, ITokenService tokenService, IBookValidator bookValidator)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _bookValidator = bookValidator;
        }

        public async Task<ManageBooksPageDto> GetAllBooksAsync(BookSpecifications specification)
        {
            var totalCountSpec = new BookSpecificationsWithCategoryFilter(specification);
            totalCountSpec.IgnorePagination();

            int matchedFilterationCount = await _unitOfWork.BookRepository.CountWithSpecAsync(totalCountSpec);
            int totalCount = await _unitOfWork.BookRepository.CountAsync();

            var specs = new BookSpecificationsWithCategoryFilter(specification);

            if (matchedFilterationCount <= AppConstants.DefaultPageSize)
            {
                specification.PageIndex = 1;
                specs = new BookSpecificationsWithCategoryFilter(specification);
            }

            var books = await _unitOfWork.BookRepository.GetWithSpecificationsAsync(specs)
               ?? Enumerable.Empty<DAL.Entities.Book>();

            var booksDto = books.Select(b => new BookDto
            {
                Id = b.Id,
                Author = b.Author,
                ISBN = b.ISBN,
                Title = b.Title,
                AvailableCount = b.AvailableCount,
                CreatedAt = b.CreatedAt
            });

            var pagination = new Pagination
            {
                PageIndex = specification.PageIndex,
                PageSize = specification.PageSize,
                TotalRecords = matchedFilterationCount,
                TotalPages = (int)Math.Ceiling((double)matchedFilterationCount / specification.PageSize)
            };

            return new ManageBooksPageDto()
            {
                Books = booksDto.ToList(),
                Metadata = new Metadata()
                {
                    Pagination = pagination,
                }
            };
        }

        public async Task<BookDetailsDto?> GetBookDetails(int bookId)
        {

            var book = await _unitOfWork.BookRepository.GetDetailedBookById(bookId);

            if (book == null)
                return null!;

            return new BookDetailsDto
            {
                ISBN = book.ISBN,
                Image = book.ImageUrl == null ? null : await ImageHelper.ConvertImageToBase64Async(book.ImageUrl),
                Author = book.Author,
                AvailableCount = book.AvailableCount,
                CreatedBy = book.CreatedBy.Fullname,
                Id = book.Id,
                Title = book.Title,
                Categories = book.BookCategories.Select(bc => bc.Category.Name).ToList(),
                CreatedAt = book.CreatedAt

            };
        }

        public async Task<List<string>> AddBook(AddBookDto bookDto)
        {
            List<string> errors = new List<string>();

            errors.AddRange(await _bookValidator.ValidateAddBook(bookDto.ISBN, bookDto.Title, bookDto.Author));

            if (errors.Any())
                return errors;

            var createdBy = await _tokenService.GetUserFromTokenAsync();

            string imageUrl = await ImageHelper.UploadImageAsync(bookDto.Image, bookDto.ISBN);

            var book = new DAL.Entities.Book
            {
                Author = bookDto.Author,
                AvailableCount = bookDto.Count,
                CreatedBy = createdBy,
                Title = bookDto.Title,
                ISBN = bookDto.ISBN,
                ImageUrl = imageUrl,
                BookCategories = bookDto.CategoriesIds.Select(categoryId => new BookCategory
                {
                    CategoryId = categoryId
                }).ToList()
            };

            _unitOfWork.BookRepository.AddEntity(book);

            int affectedRows = await _unitOfWork.SaveAsync();
            if (affectedRows <= 0)
            {
                errors.Add("An error occurred while adding the Book.");
            }

            return errors;
        }

        public async Task<List<string>> UpdateBook(UpdateBookDto bookDto)
        {
            List<string> errors = new List<string>();

            var book = _unitOfWork.BookRepository.GetByID(bookDto.Id);
            if (book == null)
            {
                errors.Add("This book not exists");
                return errors;
            }

            errors.AddRange(await _bookValidator.ValidateEditBook(bookDto.Id, bookDto.ISBN, bookDto.Title, bookDto.Author));

            book.Title = bookDto.Title;
            book.Author = bookDto.Author;
            book.AvailableCount = bookDto.Count;
            book.ISBN = bookDto.ISBN;

            _unitOfWork.BookRepository.UpdateEntity(book);
            int affctedRows = await _unitOfWork.SaveAsync();
            if (affctedRows <= 0)
                errors.Add("An error occurred while updating the Book.");

            return errors;
        }

        public async Task<List<string>> DeleteById(int id)
        {
            List<string> errors = new List<string>();
            errors.AddRange(await _bookValidator.ValidateDeleteBook(id));

            if (errors.Any())
            {
                return errors; // book in use
            }

            if (_unitOfWork.BookRepository.DeleteEntity(id))
            {
                await _unitOfWork.SaveAsync();
                return errors; // Deleted
            }

            errors.Add("This book not exists, can not delete it");
            return errors; // Not found
        }
    }
}