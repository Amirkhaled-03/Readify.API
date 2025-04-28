namespace Readify.BLL.Features.Admin.DTOs
{
    public class DashboardStatistics
    {
        public int TotalBooks { get; set; }
        public int TotalUsers { get; set; }
        public int TotalLibrarians { get; set; }
        public int TotalBorrowRecords { get; set; }
        public int TotalBorrowedBooks { get; set; }
        public int CategoriesCount { get; set; }
    }
}
