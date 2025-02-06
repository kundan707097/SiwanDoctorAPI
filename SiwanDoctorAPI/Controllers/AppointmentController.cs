using Microsoft.AspNetCore.Mvc;
using SiwanDoctorAPI.AppServices.AppointmentAppServices;
using SiwanDoctorAPI.Model.InputDTOModel.AppointmentInputDTO;

namespace SiwanDoctorAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private IAppointmentAppServices _appointmentAppServices;
        public AppointmentController(IAppointmentAppServices appointmentAppServices)
        {
            _appointmentAppServices = appointmentAppServices;
        }

        [HttpPost]
        public async Task<IActionResult> AddAppointment([FromForm] AppointmentRequest request)
        {
            if (request == null)
            {
                return BadRequest(new AppointmentResponse
                {
                    response = 400,
                    status = false,
                    message = "Invalid appointment request."
                });
            }

            var result = await _appointmentAppServices.CreateAppointment(request);
            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAppointmentsByUser(int userId)
        {
            var appointments = await _appointmentAppServices.GetAppointmentsByUserIdAsync(userId);

            if (appointments == null || !appointments.Any()) // Check if no appointments found
            {
                return NotFound(new
                {
                    response = 404,
                    message = "No appointments found for the given user."
                });
            }


            return Ok(new AppointmentGetByUserID
            {
                response = 200,
                data = appointments
            });

        }
    }
}
