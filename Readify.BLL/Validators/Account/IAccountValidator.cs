namespace Readify.BLL.Validators.Account
{
    public interface IAccountValidator
    {
        Task<List<string>> CheckExistingEmailForAddAsync(string email);
    }
}
