using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Readify.BLL.Features.Account.DTOs.AccountApproval;
using Readify.BLL.Helpers;
using Readify.BLL.ServiceContracts.AccountContracts;
using Readify.BLL.Specifications.AccountSpec;
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

        public async Task<ManageAcceptsAccountsPageDto> GetAccountsForApprovalAsync(AcceptAccountsSpec accountsSpec)
        {
            var query = _userManager.Users.AsQueryable();
            int totalCount = await query.CountAsync();

            var spec = new AcceptAccountsSpecificationImpl(accountsSpec);

            query = FilterAndSortUsersQuery(query, spec);
            int filteredCount = query.Count();

            var pagedUsers = await query
                .Skip(spec.Skip)
                .Take(spec.Take)
                .ToListAsync();

            var mappedAccounts = pagedUsers.Select(l => new AcceptAccountsDto
            {
                Id = l.Id,
                Fullname = l.Fullname,
                Email = l.UserName,
                UserStatus = l.UserStatus.ToString(),
                UserType = l.UserType.ToString(),
            });

            var pagination = new Pagination
            {
                PageIndex = accountsSpec.PageIndex,
                PageSize = accountsSpec.PageSize,
                TotalRecords = filteredCount,
                TotalPages = (int)Math.Ceiling((double)filteredCount / accountsSpec.PageSize)
            };

            return new ManageAcceptsAccountsPageDto
            {
                Accounts = mappedAccounts.ToList(),
                Metadata = new Metadata
                {
                    Pagination = pagination
                }
            };
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


        private IQueryable<ApplicationUser> FilterAndSortUsersQuery(IQueryable<ApplicationUser> query, AcceptAccountsSpecificationImpl specs)
        {
            if (specs.Criteria != null)
            {
                query = query.Where(specs.Criteria);
            }

            foreach (var include in specs.Includes)
            {
                query = query.Include(include);
            }

            if (specs.OrderByAscending != null)
            {
                query = query.OrderBy(specs.OrderByAscending);
            }
            else if (specs.OrderByDescending != null)
            {
                query = query.OrderByDescending(specs.OrderByDescending);
            }

            return query;
        }
    }
}
