namespace Readify.BLL.Features.Account.DTOs.AccountApproval
{
    public class AcceptAccountsDto
    {
        public required string Id { get; set; }
        public required string Fullname { get; set; }
        public required string Email { get; set; }
        public required string UserStatus { get; set; }
        public required string UserType { get; set; }
    }
}