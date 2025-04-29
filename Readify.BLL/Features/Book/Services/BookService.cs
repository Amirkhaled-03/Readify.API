using Microsoft.EntityFrameworkCore;
using Readify.BLL.Features.Book.DTOs;
using Readify.BLL.Helpers;
using Readify.BLL.Specifications.BookSpec;
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

            var books = await _unitOfWork.BookRepository.GetWithSpecificationsAsync(specs)
               ?? Enumerable.Empty<DAL.Entities.Book>();

            var booksDto = await Task.WhenAll(books.Select(async b => new BookDto
            {
                Id = b.Id,
                Author = b.Author,
                ISBN = b.ISBN,
                Title = b.Title,
                AvailableCount = b.AvailableCount,
                CreatedAt = b.CreatedAt,
                Image = b.ImageUrl == null ? null : await ImageHelper.ConvertImageToBase64Async(b.ImageUrl),
            }));


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

        public async Task<List<string>> ChangeBookImage(ChangeBookImage imageDto)
        {
            var errors = new List<string>();

            if (imageDto.NewImage == null || imageDto.NewImage.Length == 0)
            {
                errors.Add("No image uploaded.");
                return errors;
            }

            var book = _unitOfWork.BookRepository.GetByID(imageDto.Id);
            if (book == null)
            {
                errors.Add("Book not found.");
                return errors;
            }

            string imageUrl = await ImageHelper.UploadImageAsync(imageDto.NewImage, book.ISBN);
            if (string.IsNullOrEmpty(imageUrl))
            {
                errors.Add("An error occurred while uploading the image. Please try again.");
                return errors;
            }

            // Save the new image URL to the employee record
            string oldPath = book.ImageUrl;
            book.ImageUrl = imageUrl;

            _unitOfWork.BookRepository.UpdateEntity(book);

            int affectedRows = await _unitOfWork.SaveAsync();
            if (affectedRows <= 0)
            {
                errors.Add("An error occurred while updating the book image.");
                return errors;
            }

            // Delete the old image if it exists
            if (!string.IsNullOrEmpty(oldPath))
                ImageHelper.DeleteImage(oldPath);

            return errors;
        }

        public async Task<List<string>> UpdateBookCategories(AddCategoriesToBookDto toBookDto)
        {
            var errors = new List<string>();
            var book = _unitOfWork.BookRepository.GetByID(toBookDto.BookId);

            if (book == null)
            {
                errors.Add("book not found.");
                return errors;
            }

            IReadOnlyList<int> currentCategoriesIds = await _unitOfWork.BookCategoriesRepository.GetBookCategoriesIdsByBookId(toBookDto.BookId);
            var newCategoriesIds = new HashSet<int>(toBookDto.CategoriesIds.Where(id => id > 0));

            // Remove unassigned Categories
            foreach (var id in currentCategoriesIds.Where(d => !newCategoriesIds.Contains(d)))
            {
                var toDelete = await _unitOfWork.BookCategoriesRepository.FindAsync(ud =>
                    ud.BookId == toBookDto.BookId &&
                    ud.CategoryId == id);

                _unitOfWork.BookCategoriesRepository.DeleteEntity(toDelete);
            }

            // Add new Categories
            foreach (var id in newCategoriesIds.Where(id => !currentCategoriesIds.Contains(id)))
            {
                _unitOfWork.BookCategoriesRepository.AddEntity(new BookCategory
                {
                    CategoryId = id,
                    BookId = toBookDto.BookId,
                });
            }

            int affectedRows = await _unitOfWork.SaveAsync();
            if (affectedRows <= 0 && newCategoriesIds.Any())
            {
                errors.Add("Failed to assign new categories to the book.");
            }

            return errors;
        }

        public async Task<List<BookDto>> GetLatestBooksAsync()
        {
            var books = await _unitOfWork.BookRepository.GetAllAsQueryable()
                .OrderByDescending(b => b.CreatedAt)
                .Take(5)
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    ISBN = b.ISBN,
                    Title = b.Title,
                    Author = b.Author,
                    AvailableCount = b.AvailableCount,
                    CreatedAt = b.CreatedAt
                }).ToListAsync();

            return books;
        }

        public async Task<List<string>> DecrementAvailableCopies(int bookId)
        {
            List<string> errors = new List<string>();
            var book = _unitOfWork.BookRepository.GetByID(bookId);

            if (book == null)
            {
                errors.Add("Book not found!");
                return errors;
            }

            if (book.AvailableCount <= 0)
            {
                errors.Add("Cannot decrement no copies of book");
                return errors;
            }

            book.AvailableCount -= 1;
            _unitOfWork.BookRepository.UpdateEntity(book);

            var affectedRows = await _unitOfWork.SaveAsync();
            if (affectedRows <= 0)
            {
                errors.Add("Failed to decrement available book copies.");
            }

            return errors;
        }

        public async Task<List<string>> IncrementAvailableCopies(int bookId)
        {
            List<string> errors = new List<string>();
            var book = _unitOfWork.BookRepository.GetByID(bookId);

            if (book == null)
            {
                errors.Add("Book not found!");
                return errors;
            }

            book.AvailableCount += 1;
            _unitOfWork.BookRepository.UpdateEntity(book);

            var affectedRows = await _unitOfWork.SaveAsync();
            if (affectedRows <= 0)
            {
                errors.Add("Failed to increment available book copies.");
            }

            return errors;
        }
    }
}