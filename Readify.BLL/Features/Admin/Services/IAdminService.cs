using Readify.BLL.Features.Admin.DTOs;

namespace Readify.BLL.Features.Admin.Services
{
    public interface IAdminService
    {
        Task<AdminDashboard> Dashboard();
    }
}