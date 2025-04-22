using Readify.API.HandleResponses;
using Readify.BLL.Features.Admin.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace Readify.API.ResponseExample.AdminDashBoard
{
    public class AdminDashboardExample : IExamplesProvider<ApiResponse<AdminDashboard>>
    {
        public ApiResponse<AdminDashboard> GetExamples()
        {
            return new ApiResponse<AdminDashboard>(200, "success", data: new AdminDashboard
            {
                Statistics = new DashboardStatistics
                {
                    TotalBooks = 150,
                    TotalUsers = 200,
                    TotalBorrowRecords = 300,
                    TotalLibrarians = 10
                }
            });
        }
    }
}
