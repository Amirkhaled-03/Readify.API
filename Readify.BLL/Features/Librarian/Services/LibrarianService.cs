using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Readify.BLL.Features.JWTToken;
using Readify.BLL.Features.Librarian.DTOs;
using Readify.BLL.Helpers;
using Readify.BLL.Specifications.LibrarianSpec;
using Readify.DAL.Entities.Identity;
using Readify.DAL.UOW;

namespace Readify.BLL.Features.Librarian.Services
{
    internal class LibrarianService : ILibrarianService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;

        public LibrarianService(UnitOfWork unitOfWork, ITokenService tokenService, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _userManager = userManager;
        }

        public async Task<ManageLibrarianPageDto> GetAdminsAsync(LibrarianSpecifications specification)
        {
            var query = _userManager.Users.AsQueryable();
            int totalCount = await query.CountAsync();

            var spec = new LibrarianSpecificationsTable(specification);

            query = FilterAndSortAdminsQuery(query, spec);
            int filteredCount = query.Count();

            var pagedLibrarian = await query
                .Skip(spec.Skip)
                .Take(spec.Take)
                .ToListAsync();

            var mappedLibrarian = pagedLibrarian.Select(l => new LibrarianDto
            {
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

        private IQueryable<ApplicationUser> FilterAndSortAdminsQuery(IQueryable<ApplicationUser> query, LibrarianSpecificationsTable specs)
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