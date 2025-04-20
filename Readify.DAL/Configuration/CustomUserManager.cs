using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Readify.DAL.DBContext;
using Readify.DAL.Entities.Identity;

namespace Readify.DAL.Configuration
{
    public class CustomUserManager : UserManager<ApplicationUser>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationDBContext _context; // Assuming _context is your DB context

        public CustomUserManager(IUserStore<ApplicationUser> store,
                                 RoleManager<ApplicationRole> roleManager,
                                 IOptions<IdentityOptions> optionsAccessor,
                                 IPasswordHasher<ApplicationUser> passwordHasher,
                                 IEnumerable<IUserValidator<ApplicationUser>> userValidators,
                                 IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
                                 ILookupNormalizer keyNormalizer,
                                 IdentityErrorDescriber errors,
                                 IServiceProvider services,
                                 ILogger<UserManager<ApplicationUser>> logger,
                                 ApplicationDBContext context)  // Inject the DB context
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _roleManager = roleManager;
            _context = context; // Initialize the context
        }

        // Skip normalization in CreateAsync
        public override async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            // Skip Normalization for username and email
            user.NormalizedUserName = null;
            user.NormalizedEmail = null;

            return await base.CreateAsync(user, password);
        }

        // Skip normalization in UpdateAsync
        public override async Task<IdentityResult> UpdateAsync(ApplicationUser user)
        {
            user.NormalizedUserName = null;
            user.NormalizedEmail = null;

            return await base.UpdateAsync(user);
        }

        // Skip normalization in FindByNameAsync
        public override Task<ApplicationUser> FindByNameAsync(string userName)
        {
            // Use UserName directly without relying on NormalizedUserName
            return Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        // Override FindByEmailAsync to avoid using NormalizedEmail
        public override Task<ApplicationUser> FindByEmailAsync(string email)
        {
            // Use Email directly without relying on NormalizedEmail
            return Users.FirstOrDefaultAsync(u => u.UserName == email);
        }

        // AddToRoleAsync - Skip normalization in role assignment
        public override async Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "RoleNotFound",
                    Description = $"Role '{roleName}' not found."
                });
            }

            var userRole = new IdentityUserRole<string>
            {
                UserId = user.Id,
                RoleId = role.Id
            };

            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();

            return IdentityResult.Success;
        }

        public override async Task<IdentityResult> RemoveFromRoleAsync(ApplicationUser user, string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "RoleNotFound",
                    Description = $"Role '{roleName}' not found."
                });
            }

            var userRole = await _context.UserRoles
                .FirstOrDefaultAsync(ur => ur.UserId == user.Id && ur.RoleId == role.Id);

            if (userRole == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotInRole",
                    Description = $"User is not in role '{roleName}'."
                });
            }

            _context.UserRoles.Remove(userRole);
            await _context.SaveChangesAsync();

            return IdentityResult.Success;
        }

    }

}
