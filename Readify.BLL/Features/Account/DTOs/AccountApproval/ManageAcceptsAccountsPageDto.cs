using Readify.BLL.Helpers;

namespace Readify.BLL.Features.Account.DTOs.AccountApproval
{
    public class ManageAcceptsAccountsPageDto
    {
        public List<AcceptAccountsDto> Accounts { get; set; } = new List<AcceptAccountsDto>();
        public Metadata Metadata { get; set; } = new Metadata();
    }
}