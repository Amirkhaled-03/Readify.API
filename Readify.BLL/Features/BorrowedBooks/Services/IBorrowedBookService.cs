using Readify.BLL.Features.Book.DTOs;
using Readify.BLL.Features.BorrowedBooks.DTOs;
using Readify.BLL.Specifications.BorrowedBookSpec;
using Readify.BLL.Specifications.UserBorrowedBooksSpec;

namespace Readify.BLL.Features.BorrowedBooks.Services
{
    public interface IBorrowedBookService
    {
        Task<List<string>> AddBorrowedBookAsync(DAL.Entities.BorrowRequest bookDto);
        Task<ManageBorrowedBooksDto> GetAllBorrowedBooksAsync(BorrowedBookSpecification specs);
        Task<BorrowedBookDto> GetBorrowedBookByIdAsync(int id);
        Task<ManageBorrowedBooksDto> GetUserBorrowBooksAsync(UserBorrowedBooksSpecification specs);
        Task<List<string>> UpdateBorrowedBookStatusAsync(UpdateBorrowedBookStatusDto bookDto);
        Task<List<BookDto>> GetRecommendedBooksAsync();
    }
}
