using Microsoft.AspNetCore.Mvc;
using SiwanDoctorAPI.AppServices.PublicDoctorAppServices;

namespace SiwanDoctorAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class PublicDoctorController : ControllerBase
    {
        private readonly IPublicDoctorAppServices _publicDoctorAppServices;
        public PublicDoctorController(IPublicDoctorAppServices publicDoctorAppServices)
        {
            _publicDoctorAppServices = publicDoctorAppServices;
        }

        [HttpGet("get_doctor")]
        public async Task<IActionResult> GetDoctors()
        {
            var result = await _publicDoctorAppServices.GetDoctorsAsync();
            return Ok(result);
        }

        [HttpGet("get_doctor/{id}")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var result = await _publicDoctorAppServices.GetDoctorByIdAsync(id);

            if (result == null)
                return NotFound(new { response = 404, message = "Doctor not found" });

            return Ok(result);
        }
    }
}
