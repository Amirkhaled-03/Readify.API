using Readify.BLL.Features.Account.DTOs.Registration;

namespace Readify.BLL.Features.Account.ServicesContracts
{
    public interface IUserRegistrationService
    {
        Task<List<string>> RegisterAdminAsync(RegisterApplicationUser registerDto);
        Task<List<string>> RegisterLibrarianAsync(RegisterApplicationUser registerDto);
        Task<List<string>> RegisterUserAsync(RegisterApplicationUser registerDto);
    }
}
