using Readify.BLL.Features.Librarian.DTOs;
using Readify.BLL.Specifications.LibrarianSpec;

namespace Readify.BLL.Features.Librarian.Services
{
    public interface ILibrarianService
    {
        Task<ManageLibrarianPageDto> GetLibrarianAsync(LibrarianSpecifications specification);
        Task<LibrarianDto?> GetLibrarianByIdAsync(string id);
        Task<List<string>> EditLibrarian(EditLibrarianDto editLibrarian);
        Task<bool> DeleteLibrarianById(string id);
    }
}