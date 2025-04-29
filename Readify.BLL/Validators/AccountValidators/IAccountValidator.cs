namespace Readify.BLL.Validators.Account
{
    public interface IAccountValidator
    {
        Task<List<string>> CheckExistingEmailForAddAsync(string email);
        Task<List<string>> CheckExistingPhoneForAddAsync(string phone);
        Task<List<string>> CheckExistingPhoneForEditAsync(string userId, string phone);
        Task<List<string>> CheckExistingEmailForEditAsync(string userId, string email);
        Task<List<string>> ValidateUpdateStatus(string userId, UserStatus status);
    }
}