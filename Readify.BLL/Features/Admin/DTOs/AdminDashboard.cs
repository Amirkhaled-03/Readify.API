using Readify.BLL.Features.Book.DTOs;
using Readify.BLL.Features.BookCategories.DTOs;

namespace Readify.BLL.Features.Admin.DTOs
{
    public class AdminDashboard
    {
        public DashboardStatistics Statistics { get; set; } = new DashboardStatistics();
        public List<LatestBooksDto> LatestBooks { get; set; }
        public List<CategoryDto> LatestCategories { get; set; }

    }
}
