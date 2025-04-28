using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Readify.BLL.Features.JWTToken;
using Readify.BLL.Features.User.DTOs;
using Readify.BLL.Specifications.UserSpec;
using Readify.DAL.Entities.Identity;
using Readify.DAL.SpecificationPattern;
using Readify.DAL.UOW;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Readify.BLL.Features.User.Services
{
    internal class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public UserService(UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
           _userManager = userManager;
            _tokenService = tokenService;
        }

        public Task<List<UserDto>> GetAllUsers(UserSpecification specs)
        {
            throw new NotImplementedException();
        }

        //public async Task<List<UserDto>> GetAllUsers(UserSpecification specs)
        //{
        //    var totalCountSpec = new UserSpecificationImpl(specs);
        //    totalCountSpec.IgnorePagination();

        //    int matchedCount = await ApplyUserSpecifications(totalCountSpec).CountAsync();
        //    int totalCount = await _userManager.Users.CountAsync();

        //    var specifications = new UserSpecificationImpl(specs);

    }
}
