using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Readify.BLL.Features.Librarian.DTOs;
using Readify.BLL.Features.User.DTOs;
using Readify.BLL.Helpers;
using Readify.BLL.Specifications.UserSpec;
using Readify.DAL.Entities.Identity;
using Readify.DAL.UOW;

namespace Readify.BLL.Features.User.Services
{
    internal class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountValidator _accountValidator;

        public UserService(UserManager<ApplicationUser> userManager, ITokenService tokenService, IUnitOfWork unitOfWork, IAccountValidator accountValidator)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _accountValidator = accountValidator;
        }

        public async Task<ManageLibrarianPageDto> GetUsersAsync(UserSpecification specification)
        {
            var query = _userManager.Users.AsQueryable();
            int totalCount = await query.CountAsync();

            var spec = new UserSpecificationImpl(specification);

            query = FilterAndSortUsersQuery(query, spec);
            int filteredCount = query.Count();

            var pagedLibrarian = await query
                .Skip(spec.Skip)
                .Take(spec.Take)
                .ToListAsync();

            var mappedLibrarian = pagedLibrarian.Select(l => new LibrarianDto
            {
                Id = l.Id,
                CreatedAt = l.CreatedAt,
                Fullname = l.Fullname,
                PhoneNumber = l.PhoneNumber,
                Username = l.UserName,
                UserStatus = l.UserStatus,
            });

            var pagination = new Pagination
            {
                PageIndex = specification.PageIndex,
                PageSize = specification.PageSize,
                TotalRecords = filteredCount,
                TotalPages = (int)Math.Ceiling((double)filteredCount / specification.PageSize)
            };

            return new ManageLibrarianPageDto
            {
                Librarians = mappedLibrarian.ToList(),
                Metadata = new Metadata
                {
                    Pagination = pagination
                }
            };
        }

        public async Task<LibrarianDto?> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return null;

            var userDto = new LibrarianDto
            {
                Id = user.Id,
                CreatedAt = user.CreatedAt,
                Fullname = user.Fullname,
                PhoneNumber = user.PhoneNumber,
                Username = user.UserName,
                UserStatus = user.UserStatus,
            };

            return userDto;
        }


        public async Task<List<string>> EditUser(EditUserDto editUser)
        {
            var errors = new List<string>();

            errors.AddRange(await _accountValidator.CheckExistingPhoneForEditAsync(editUser.Id, editUser.PhoneNumber));
            errors.AddRange(await _accountValidator.CheckExistingEmailForEditAsync(editUser.Id, editUser.Username));

            if (errors.Any())
                return errors;

            var user = await _userManager.FindByIdAsync(editUser.Id);
            if (user == null)
                errors.Add("This User not exists");

            user.Fullname = editUser.FullName;
            user.UserName = editUser.Username;
            user.PhoneNumber = editUser.PhoneNumber;

            var res = await _userManager.UpdateAsync(user);

            if (!res.Succeeded)
                errors.Add("An error occurred while updating the user.");

            return errors;
        }

        public async Task<bool> DeleteUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return false; // User not found

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }


        private IQueryable<ApplicationUser> FilterAndSortUsersQuery(IQueryable<ApplicationUser> query, UserSpecificationImpl specs)
        {
            query = query.Where(user => user.UserType == UserType.Librarian);

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
