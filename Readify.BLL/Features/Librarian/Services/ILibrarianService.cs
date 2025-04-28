using Readify.BLL.Features.Librarian.DTOs;
using Readify.BLL.Specifications.LibrarianSpec;

namespace Readify.BLL.Features.Librarian.Services
{
    public interface ILibrarianService
    {
        Task<ManageLibrarianPageDto> GetAdminsAsync(LibrarianSpecifications specification);
    }
}