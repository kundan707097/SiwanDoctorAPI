using Microsoft.AspNetCore.Mvc;
using SiwanDoctorAPI.AppServices.AdminAppServices;

namespace SiwanDoctorAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminAppServices _adminAppServices;
        public AdminController(IAdminAppServices adminAppServices)
        {
            _adminAppServices = adminAppServices;
        }

        [HttpGet("GetAll_User")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _adminAppServices.GetUsersAsync();
            return Ok(new { response = 200, data = users });
        }

    

        [HttpGet("GetAll_appointment")]
        public async Task<IActionResult> GetAppointments(
         string? search,
         int start,
         int end,
         string? status)
        {
            var appointments = await _adminAppServices.GetAppointmentsAsync(search, start, end, status);

            return Ok(new
            {
                response = 200,
                data = appointments
            });
        }
    }
}
