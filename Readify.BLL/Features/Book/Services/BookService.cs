﻿using Microsoft.EntityFrameworkCore;
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
                Description = b.Description,
                Language = b.Language,
                PageCount = b.PageCount,
                Price = b.Price,
                PublishYear = b.PublishYear,
                Rating = b.Rating,
                AvailableCount = b.AvailableCount,
                CreatedAt = b.CreatedAt,
                Categories = b.BookCategories.Select(bc => bc.Category.Name).ToList(),
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
                CreatedBy = book.CreatedBy,
                Id = book.Id,
                Description = book.Description,
                Rating = book.Rating,
                PublishYear = book.PublishYear,
                Price = book.Price,
                PageCount = book.PageCount,
                Language = book.Language,
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
                CreatedBy = createdBy.Fullname,
                Title = bookDto.Title,
                Description = bookDto.Description,
                Language = bookDto.Language,
                PageCount = bookDto.PageCount,
                Price = bookDto.Price,
                PublishYear = bookDto.PublishYear,
                Rating = bookDto.Rating,
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
                errors.Add("This book does not exist.");
                return errors;
            }

            // Validate book fields
            errors.AddRange(await _bookValidator.ValidateEditBook(bookDto.Id, bookDto.ISBN, bookDto.Title, bookDto.Author));

            // Update basic fields
            book.Title = bookDto.Title;
            book.Author = bookDto.Author;
            book.AvailableCount = bookDto.Count;
            book.ISBN = bookDto.ISBN;
            book.Description = bookDto.Description;
            book.Language = bookDto.Language;
            book.Rating = bookDto.Rating;
            book.PublishYear = bookDto.PublishYear;
            book.Price = bookDto.Price;
            book.PageCount = bookDto.PageCount;

            // If a new image is uploaded, handle image upload
            if (bookDto.NewImage != null && bookDto.NewImage.Length > 0)
            {
                string oldPath = book.ImageUrl;
                string imageUrl = await ImageHelper.UploadImageAsync(bookDto.NewImage, book.ISBN);
                if (string.IsNullOrEmpty(imageUrl))
                {
                    errors.Add("An error occurred while uploading the image.");
                    return errors;
                }

                book.ImageUrl = imageUrl;

                // Optionally delete old image
                if (!string.IsNullOrEmpty(oldPath))
                    ImageHelper.DeleteImage(oldPath);
            }

            _unitOfWork.BookRepository.UpdateEntity(book);
            int affectedRows = await _unitOfWork.SaveAsync();
            if (affectedRows <= 0)
                errors.Add("An error occurred while saving the book update.");

            return errors;
        }

        public async Task<List<string>> DeleteById(int id)
        {
            var errors = await _bookValidator.ValidateDeleteBook(id);

            if (errors.Any())
            {
                return errors; // Cannot delete, in use
            }

            var deleted = _unitOfWork.BookRepository.DeleteEntity(id);

            if (!deleted)
            {
                errors.Add("This book does not exist and cannot be deleted.");
                return errors;
            }

            await _unitOfWork.SaveAsync();
            return errors; // No errors, deleted successfully
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

        public async Task<List<LatestBooksDto>> GetLatestBooksAsync()
        {
            var books = await _unitOfWork.BookRepository.GetAllAsQueryable()
                .OrderByDescending(b => b.CreatedAt)
                .Take(5)
                .Select(b => new LatestBooksDto
                {
                    Id = b.Id,
                    Title = b.Title,
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

        public async Task<List<BookDto>> GetBookByCategoryAsync(int categoryId)
        {
            var categoryExists = await _unitOfWork.CategoriesRepository.GetFirstOrDefaultAsync(c => c.Id == categoryId);
            if (categoryExists == null)
            {
                return null;
            }

            var booksByCategory = await _unitOfWork.BookRepository.GetBookByCategory(categoryId);

            if (booksByCategory == null || !booksByCategory.Any())
                return new List<BookDto>();

            var bookDtos = await Task.WhenAll(
                booksByCategory.Select(async b => new BookDto
                {
                    Id = b.Id,
                    Author = b.Author,
                    ISBN = b.ISBN,
                    Title = b.Title,
                    Description = b.Description,
                    Language = b.Language,
                    PageCount = b.PageCount,
                    Price = b.Price,
                    PublishYear = b.PublishYear,
                    Rating = b.Rating,
                    AvailableCount = b.AvailableCount,
                    CreatedAt = b.CreatedAt,
                    Categories = b.BookCategories.Select(bc => bc.Category.Name).ToList(),
                    Image = b.ImageUrl == null ? null : await ImageHelper.ConvertImageToBase64Async(b.ImageUrl)
                })
            );

            return bookDtos.ToList();
        }

        public async Task<List<BookDto>> GetBookByAuthorAsync(string authorName)
        {
            var booksByAuthor = await _unitOfWork.BookRepository.GetBookByAuthor(authorName);

            if (booksByAuthor == null || !booksByAuthor.Any())
                return new List<BookDto>();

            var bookDtos = await Task.WhenAll(
                booksByAuthor.Select(async b => new BookDto
                {
                    Id = b.Id,
                    Author = b.Author,
                    ISBN = b.ISBN,
                    Title = b.Title,
                    Description = b.Description,
                    Language = b.Language,
                    PageCount = b.PageCount,
                    Price = b.Price,
                    PublishYear = b.PublishYear,
                    Rating = b.Rating,
                    AvailableCount = b.AvailableCount,
                    CreatedAt = b.CreatedAt,
                    Categories = b.BookCategories.Select(bc => bc.Category.Name).ToList(),
                    Image = b.ImageUrl == null ? null : await ImageHelper.ConvertImageToBase64Async(b.ImageUrl)
                })
            );

            return bookDtos.ToList();
        }
    }
}