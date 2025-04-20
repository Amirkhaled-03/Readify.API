using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Readify.DAL.Entities.Identity;

namespace Readify.DAL.Configuration
{
    public class CustomRoleManager : RoleManager<ApplicationRole>
    {
        public CustomRoleManager(
            IRoleStore<ApplicationRole> store,
            IEnumerable<IRoleValidator<ApplicationRole>> roleValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            ILogger<RoleManager<ApplicationRole>> logger)
            : base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }

        public override async Task<IdentityResult> CreateAsync(ApplicationRole role)
        {
            role.NormalizedName = null;  // Explicitly set to null to skip normalization
            return await base.CreateAsync(role);
        }

        public override async Task<IdentityResult> UpdateAsync(ApplicationRole role)
        {
            role.NormalizedName = null;  // Explicitly set to null to skip normalization
            return await base.UpdateAsync(role);
        }

        public override async Task<ApplicationRole> FindByNameAsync(string roleName)
        {
            // Query by Name without using NormalizedName
            return await Roles.FirstOrDefaultAsync(r => r.Name == roleName);
        }

        public async Task<ApplicationRole> FindByNameWithoutNormalizationAsync(string roleName)
        {
            // Pass CancellationToken.None to avoid the error
            return await Store.FindByNameAsync(roleName, CancellationToken.None);
        }

    }
}
