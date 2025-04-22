using Microsoft.AspNetCore.Identity;
using Readify.BLL.Features.Admin.DTOs;
using Readify.DAL.Entities.Identity;
using Readify.DAL.UOW;

namespace Readify.BLL.Features.Admin.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<AdminDashboard> Dashboard()
        {
            int totalBooks = await _unitOfWork.BookRepository.CountAsync();
            int totalUsers = _userManager.Users.Count(u => u.UserType == UserType.User);
            int totalLibrarians = _userManager.Users.Count(u => u.UserType == UserType.Librarian);
            int borrowRecords = await _unitOfWork.BorrowRequestRepository.CountAsync();

            return new AdminDashboard
            {
                Statistics = new DashboardStatistics
                {
                    TotalBooks = totalBooks,
                    TotalUsers = totalUsers,
                    TotalBorrowRecords = borrowRecords,
                    TotalLibrarians = totalLibrarians
                }
            };
        }
    }
}
