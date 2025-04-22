using Microsoft.AspNetCore.Mvc;
using Readify.API.HandleResponses;
using Readify.API.ResponseExample.AdminDashBoard;
using Readify.BLL.Features.Admin.DTOs;
using Readify.BLL.Features.Admin.Services;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Readify.API.Controllers
{

    public class AdminController : BaseController
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("Dashboard")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<AdminDashboard>))]
        [SwaggerResponseExample(200, typeof(AdminDashboardExample))]
        [SwaggerOperation(
        Summary = "Get Admin Dashboard Statistics",
        Description = "This endpoint provides the statistics for total books, total users, total borrow records, and total librarians."
        )]
        public async Task<IActionResult> GetDashboard()
        {
            var dashboardData = await _adminService.Dashboard();
            return Ok(new ApiResponse<AdminDashboard>(200, "success", data: dashboardData));
        }

    }
}
