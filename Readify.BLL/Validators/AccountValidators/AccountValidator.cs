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

        public async Task<List<string>> CheckExistingPhoneForAddAsync(string phone)
        {
            var errors = new List<string>();

            bool userExists = await _userManager.Users.AnyAsync(u => u.PhoneNumber == phone);
            if (userExists)
            {
                errors.Add($"A user with the phone '{phone}' already exists enter other phone.");
            }

            return errors;
        }

        public async Task<List<string>> CheckExistingEmailForEditAsync(string userId, string email)
        {
            var errors = new List<string>();

            bool userExists = await _userManager.Users.AnyAsync(u => u.Id != userId && u.UserName == email);
            if (userExists)
            {
                errors.Add($"A user with the email '{email}' already exists enter other Email.");
            }

            return errors;
        }

        public async Task<List<string>> CheckExistingPhoneForEditAsync(string userId, string phone)
        {
            var errors = new List<string>();

            bool userExists = await _userManager.Users.AnyAsync(u => u.Id != userId && u.PhoneNumber == phone);
            if (userExists)
            {
                errors.Add($"A user with the phone '{phone}' already exists enter other phone.");
            }

            return errors;
        }

        public async Task<List<string>> ValidateUpdateStatus(string userId, UserStatus status)
        {
            var errors = new List<string>();

            var user = await _userManager.FindByIdAsync(userId);

            if (user.UserStatus == status)
            {
                errors.Add("User already has this status");
                return errors;
            }

            if (user.UserStatus == UserStatus.Approved)
            {
                errors.Add("Cannot change approved user status");
                return errors;
            }

            if (user.UserStatus == UserStatus.Rejected)
                errors.Add("Cannot change rejected status");

            return errors;
        }
    }
}