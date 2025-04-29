using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Readify.BLL.Features.Account.DTOs;
using Readify.BLL.ServiceContracts.AccountContracts;
using Readify.DAL.Entities.Identity;
using Readify.DAL.UOW;

namespace Readify.BLL.Features.Account.Services
{
    internal class UserManagementService : IUserManagementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAccountValidator _accountValidator;
        public UserManagementService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IAccountValidator accountValidator)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _accountValidator = accountValidator;
        }
        public async Task<List<string>> UpdateAccountStatusAsync(UpdateAccountStatusDto updateStatusDto)
        {
            List<string> errors = new List<string>();

            var user = await _userManager.FindByIdAsync(updateStatusDto.UserId);

            if (user == null)
            {
                errors.Add("User not found!");
                return errors;
            }

            errors.AddRange(await _accountValidator.ValidateUpdateStatus(user.Id, updateStatusDto.UserStatus));

            if (errors.Any())
                return errors;

            user.UserStatus = updateStatusDto.UserStatus;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                errors.AddRange(result.Errors.Select(e => e.Description));
            }

            return errors;
        }
    }
}
