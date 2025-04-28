using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Readify.BLL.Features.User.DTOs;
using Readify.BLL.Specifications.UserSpec;

namespace Readify.BLL.Features.User.Services
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsers(UserSpecification specs);
    }
}
