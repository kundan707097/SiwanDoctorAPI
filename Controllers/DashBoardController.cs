using Microsoft.AspNetCore.Mvc;
using SiwanDoctorAPI.AppServices.DashboardAppService;

namespace SiwanDoctorAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IDashboardAppService _dashboardAppService;
        public DashBoardController(IDashboardAppService dashboardAppService)
        {
            _dashboardAppService = dashboardAppService;
            
        }

        [HttpGet("DashBoard/doctor/{doctorId}")]
        public async Task<IActionResult> GetDashboardCountByDoctorId( int doctorId)
        {
            var result = await _dashboardAppService.GetDashboardCountAsync(doctorId);
            return Ok(new { response = 200, data = result });
        }


        [HttpGet("get_admin_dashboard_count")]
        public async Task<IActionResult> GetAdminDashboardCount()
        {
            var result = await _dashboardAppService.GetAdminDashboardCountAsync();
            return Ok(result);
        }
    }
}
