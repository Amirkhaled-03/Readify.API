﻿using Microsoft.AspNetCore.Identity;
using Readify.BLL.Features.Account.DTOs.Registration;
using Readify.DAL.Entities.Identity;

namespace Readify.BLL.Features.Account.Services
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IAccountValidator _accountValidator;
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRegistrationService(IAccountValidator accountValidator,
                                       UserManager<ApplicationUser> userManager,
                                       ITokenService tokenService
                                       )
        {
            _accountValidator = accountValidator;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<List<string>> RegisterAdminAsync(RegisterApplicationUser registerDto)
        {
            List<string> errors = new List<string>();

            errors.AddRange(await _accountValidator.CheckExistingEmailForAddAsync(registerDto.Email));
            errors.AddRange(await _accountValidator.CheckExistingPhoneForAddAsync(registerDto.PhoneNumber));

            if (errors.Any())
                return errors;

            var user = new ApplicationUser
            {
                PhoneNumber = registerDto.PhoneNumber,
                Fullname = registerDto.Fullname,
                UserName = registerDto.Email,
                UserType = UserType.Admin,
                UserStatus = UserStatus.Approved,
            };

            var res = await _userManager.CreateAsync(user, registerDto.Password);
            if (!res.Succeeded)
                errors.Add("Failed to add the Admin. Please try again later.");

            return errors;
        }

        public async Task<List<string>> RegisterLibrarianAsync(RegisterApplicationUser registerDto)
        {
            List<string> errors = new List<string>();

            errors.AddRange(await _accountValidator.CheckExistingEmailForAddAsync(registerDto.Email));
            errors.AddRange(await _accountValidator.CheckExistingPhoneForAddAsync(registerDto.PhoneNumber));

            if (errors.Any())
                return errors;

            var token = _tokenService.GetUserIdFromToken();

            var user = new ApplicationUser
            {
                PhoneNumber = registerDto.PhoneNumber,
                Fullname = registerDto.Fullname,
                UserName = registerDto.Email,
                UserType = UserType.Librarian,
                UserStatus = token != null ? UserStatus.Approved : UserStatus.Pending,
            };

            var res = await _userManager.CreateAsync(user, registerDto.Password);
            if (!res.Succeeded)
                errors.Add("Failed to add the Librarian. Please try again later.");

            return errors;
        }

        public async Task<List<string>> RegisterUserAsync(RegisterApplicationUser registerDto)
        {
            List<string> errors = new List<string>();

            errors.AddRange(await _accountValidator.CheckExistingEmailForAddAsync(registerDto.Email));
            errors.AddRange(await _accountValidator.CheckExistingPhoneForAddAsync(registerDto.PhoneNumber));

            if (errors.Any())
                return errors;

            var user = new ApplicationUser
            {
                PhoneNumber = registerDto.PhoneNumber,
                Fullname = registerDto.Fullname,
                UserName = registerDto.Email,
                UserType = UserType.User,
                UserStatus = UserStatus.Approved,
            };

            var res = await _userManager.CreateAsync(user, registerDto.Password);
            if (!res.Succeeded)
                errors.Add("Failed to add the User. Please try again later.");

            return errors;
        }
    }
}