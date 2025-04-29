using Microsoft.AspNetCore.Identity;
using Readify.BLL.Features.Admin.DTOs;
using Readify.BLL.Features.Book.Services;
using Readify.BLL.Features.BookCategories.Services;
using Readify.DAL.Entities.Identity;
using Readify.DAL.UOW;

namespace Readify.BLL.Features.Admin.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBookService _bookService;
        private readonly IBookCategoriesService _categoryService;

        public AdminService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IBookService bookService, IBookCategoriesService categoryService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _bookService = bookService;
            _categoryService = categoryService;
        }

        public async Task<AdminDashboard> Dashboard()
        {
            int totalBooks = await _unitOfWork.BookRepository.CountAsync();
            int totalUsers = _userManager.Users.Count(u => u.UserType == UserType.User);
            int totalLibrarians = _userManager.Users.Count(u => u.UserType == UserType.Librarian);
            int borrowRecords = await _unitOfWork.BorrowRequestRepository.CountAsync();
            int categoriesCount = await _unitOfWork.CategoriesRepository.CountAsync();
            int borrowedBooks = await _unitOfWork.BorrowedBookRepository.CountAsync();

            var latestBooks = await _bookService.GetLatestBooksAsync();
            var latestCategories = await _categoryService.GetLatestCategoriesAsync();

            return new AdminDashboard
            {
                Statistics = new DashboardStatistics
                {
                    TotalBooks = totalBooks,
                    TotalUsers = totalUsers,
                    TotalBorrowRecords = borrowRecords,
                    TotalBorrowedBooks = borrowedBooks,
                    TotalLibrarians = totalLibrarians,
                    CategoriesCount = categoriesCount
                },
                LatestBooks = latestBooks,
                LatestCategories = latestCategories,
            };
        }
    }
}
