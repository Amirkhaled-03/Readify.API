using Readify.BLL.Features.Account.DTOs.Login;

namespace Readify.BLL.Features.Account.ServicesContracts
{
    public interface IAuthenticationService
    {
        Task<LoginResultDto> LogIn(LogInDto logInDto);
    }
}
