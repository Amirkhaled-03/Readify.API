using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Readify.BLL.Features.Librarian.DTOs;
using Readify.BLL.Helpers;
using Readify.BLL.Specifications.LibrarianSpec;
using Readify.DAL.Entities.Identity;
using Readify.DAL.UOW;

namespace Readify.BLL.Features.Librarian.Services
{
    internal class LibrarianService : ILibrarianService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAccountValidator _accountValidator;

        public LibrarianService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IAccountValidator accountValidator)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _accountValidator = accountValidator;
        }

        public async Task<ManageLibrarianPageDto> GetLibrarianAsync(LibrarianSpecifications specification)
        {
            var query = _userManager.Users.AsQueryable();
            int totalCount = await query.CountAsync();

            var spec = new LibrarianSpecificationsImpl(specification);

            query = FilterAndSortLibrariansQuery(query, spec);
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

        public async Task<LibrarianDto?> GetLibrarianByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return null;

            var librarian = new LibrarianDto
            {
                Id = user.Id,
                CreatedAt = user.CreatedAt,
                Fullname = user.Fullname,
                PhoneNumber = user.PhoneNumber,
                Username = user.UserName,
                UserStatus = user.UserStatus,
            };

            return librarian;
        }


        public async Task<List<string>> EditLibrarian(EditLibrarianDto editLibrarian)
        {
            var errors = new List<string>();

            errors.AddRange(await _accountValidator.CheckExistingPhoneForEditAsync(editLibrarian.Id, editLibrarian.PhoneNumber));
            errors.AddRange(await _accountValidator.CheckExistingEmailForEditAsync(editLibrarian.Id, editLibrarian.Username));

            if (errors.Any())
                return errors;

            var librarian = await _userManager.FindByIdAsync(editLibrarian.Id);
            if (librarian == null)
                errors.Add("This Librarian not exists");

            librarian.Fullname = editLibrarian.FullName;
            librarian.UserName = editLibrarian.Username;
            librarian.PhoneNumber = editLibrarian.PhoneNumber;

            var res = await _userManager.UpdateAsync(librarian);

            if (!res.Succeeded)
                errors.Add("An error occurred while updating the librarian.");

            return errors;
        }

        public async Task<bool> DeleteLibrarianById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return false; // User not found

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        private IQueryable<ApplicationUser> FilterAndSortLibrariansQuery(IQueryable<ApplicationUser> query, LibrarianSpecificationsImpl specs)
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