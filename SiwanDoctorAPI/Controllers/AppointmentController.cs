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

        [HttpGet("get_appointment/{appointmentId}")]
        public async Task<IActionResult> GetAppointment(int appointmentId)
        {
            var appointment = await _appointmentAppServices.GetAppointmentById(appointmentId);

            if (appointment == null || appointment.data == null)
            {
                return NotFound(new { Message = "Appointment not found." });
            }

            return Ok(appointment);
        }

        [HttpPost("update_appointment_status")]
        public async Task<IActionResult> UpdateAppointmentStatus([FromBody] UpdateAppointmentStatus request)
        {
            var response = await _appointmentAppServices.UpdateAppointmentStatusById(request);

            if (response.status)
            {
                return Ok(response); // 200 OK with response
            }
            else
            {
                return StatusCode(response.response, response); // Return appropriate status code (404, 500, etc.)
            }
        }

        [HttpGet("get_appointment_date")]
        public async Task<IActionResult> GetAppointmentDate([FromQuery] string start_date, [FromQuery] string end_date)
        {
            if (DateTime.TryParse(start_date, out DateTime startDate) && DateTime.TryParse(end_date, out DateTime endDate))
            {
                var response = await _appointmentAppServices.GetAppointmentsByDateRange(startDate, endDate);
                return Ok(response);
            }

            return BadRequest(new { response = 400, message = "Invalid date format." });
        }

        [HttpGet("get_appointments/doctor/{doctorId}")]
        public async Task<IActionResult> GetAppointments(int doctorId)
        {
            var result = await _appointmentAppServices.GetAppointmentsByDoctorIdAsync(doctorId);
            return Ok(new { response = 200, data = result });
        }

        //[HttpGet("get_appointments/{doctor_id}/page")]
        //public async IActionResult GetAppointmentsByPagination(int doctor_id, int start, int end)
        //{
        //    var result = await _appointmentAppServices.GetAppointmentsByPaginationRecords(doctor_id, start, end);
        //}
    }
}
