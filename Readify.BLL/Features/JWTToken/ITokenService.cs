﻿using Readify.DAL.Entities.Identity;

namespace Readify.BLL.Features.JWTToken
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser appUser);
        string GetUserIdFromToken();
        Task<ApplicationUser> GetUserFromTokenAsync();
        string GetUserRoleFromToken();
    }
}