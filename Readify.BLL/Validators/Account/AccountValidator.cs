using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Readify.DAL.Entities.Identity;

namespace Readify.BLL.Validators.Account
{
    public class AccountValidator : IAccountValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<string>> CheckExistingEmailForAddAsync(string email)
        {
            var errors = new List<string>();

            bool userExists = await _userManager.Users.AnyAsync(u => u.UserName == email);
            if (userExists)
            {
                errors.Add($"A user with the email '{email}' already exists enter other Email.");
            }

            return errors;
        }
    }
}
