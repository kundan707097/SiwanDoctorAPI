using Microsoft.AspNetCore.Mvc;
using SiwanDoctorAPI.AppServices.DashboardAppService;

namespace SiwanDoctorAPI.Controllers
{
    public class DashBoardController : ControllerBase
    {
        private readonly IDashboardAppService _dashboardAppService;
        public DashBoardController(IDashboardAppService dashboardAppService)
        {
            _dashboardAppService = dashboardAppService;
            
        }

        //[HttpGet("DashBoard/doctor/{doctorId}")]
        //public async Task<IActionResult> GetDashboardCount(int doctorId)
        //{
        //    var result = await _dashboardAppService.GetDashboardCountAsync(doctorId);
        //    return Ok(new { response = 200, data = result });
        //}
    }
}
