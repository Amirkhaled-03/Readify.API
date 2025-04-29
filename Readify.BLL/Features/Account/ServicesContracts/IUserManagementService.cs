using Readify.BLL.Features.Account.DTOs;

namespace Readify.BLL.ServiceContracts.AccountContracts
{
    public interface IUserManagementService
    {
        Task<List<string>> UpdateAccountStatusAsync(UpdateAccountStatusDto updateStatusDto);
    }
}
