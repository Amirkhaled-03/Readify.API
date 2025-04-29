using Readify.BLL.Features.Librarian.DTOs;
using Readify.BLL.Features.User.DTOs;
using Readify.BLL.Specifications.UserSpec;

namespace Readify.BLL.Features.User.Services
{
    public interface IUserService
    {
        Task<ManageLibrarianPageDto> GetUsersAsync(UserSpecification specification);
        Task<LibrarianDto?> GetUserByIdAsync(string id);
        Task<bool> DeleteUserById(string id);
        Task<List<string>> EditUser(EditUserDto editUser);
    }
}