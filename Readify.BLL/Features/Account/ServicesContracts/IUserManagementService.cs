using Readify.BLL.Features.Account.DTOs.AccountApproval;
using Readify.BLL.Specifications.AccountSpec;

namespace Readify.BLL.ServiceContracts.AccountContracts
{
    public interface IUserManagementService
    {
        Task<List<string>> UpdateAccountStatusAsync(UpdateAccountStatusDto updateStatusDto);
        Task<ManageAcceptsAccountsPageDto> GetAccountsForApprovalAsync(AcceptAccountsSpec accountsSpec);
    }
}