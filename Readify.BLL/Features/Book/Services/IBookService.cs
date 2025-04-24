using Readify.BLL.Features.Book.DTOs;
using Readify.BLL.Specifications.BookSpec;

namespace Readify.BLL.Features.Book.Services
{
    public interface IBookService
    {
        Task<ManageBooksPageDto> GetAllBooksAsync(BookSpecifications specification);
        Task<BookDetailsDto?> GetBookDetails(int bookId);
        Task<List<string>> AddBook(AddBookDto bookDto);
        Task<List<string>> UpdateBook(UpdateBookDto bookDto);
        Task<List<string>> DeleteById(int id);
        Task<List<string>> ChangeBookImage(ChangeBookImage imageDto);
        Task<List<string>> UpdateBookCategories(AddCategoriesToBookDto toBookDto);
    }
}