using Microsoft.AspNetCore.Mvc;
using SiwanDoctorAPI.AppServices.PublicDoctorAppServices;
using SiwanDoctorAPI.Model.EntityModel.AppointmentDetails;
using SiwanDoctorAPI.Model.InputDTOModel.TimeSlotInputDTO;

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
        public async Task<IActionResult> GetDoctorById( int id)
        {
            var result = await _publicDoctorAppServices.GetDoctorByIdAsync(id);

            if (result == null)
                return NotFound(new { response = 404, message = "Doctor not found" });

            return Ok(result);
        }

        [HttpGet("get_doctor_time_slots/{doctorId}")]
        public async Task<IActionResult> GetDoctorTimeSlots(  int doctorId)
        {
            var timeSlots = await _publicDoctorAppServices.GetDoctorTimeSlotsAsync(doctorId);

            if (timeSlots == null || !timeSlots.Any())
            {
                return NotFound(new { response = 404, message = "No time slots found for the given doctor." });
            }

            return Ok(new DoctorTimeSlotResponse
            {
                response = 200,
                data = timeSlots
            });
        }

        [HttpGet("get_doctor_time_interval/{doctorId}/{day}")]
        public async Task<IActionResult> GetDoctorTimeIntervals( int doctorId, string day)
        {
            var timeIntervals = await _publicDoctorAppServices.GetDoctorTimeIntervalsAsync(doctorId, day);

            if (timeIntervals == null || !timeIntervals.Any())
            {
                return NotFound(new { response = 404, message = "No time slots found for the given doctor on this day." });
            }

            return Ok(new { response = 200, data = timeIntervals });
        }

        [HttpGet("get_doctor_video_time_slots/{doctorId}")]
        public async Task<IActionResult> GetDoctorVideoTimeSlots( int doctorId)
        {
            var serviceResponse = await _publicDoctorAppServices.GetDoctorVideoTimeSlotsAsync(doctorId);
            if (serviceResponse == null || !serviceResponse.Any())
            {
                return NotFound(new { response = 404, message = "No time slots found for the given doctor." });
            }

            return Ok(new DoctorTimeSlotResponse
            {
                response = 200,
                data = serviceResponse
            });
        }

        [HttpGet("get_doctor_videotime_interval/{doctorId}/{day}")]
        public async Task<IActionResult> GetDoctorVideoTimeIntervals( int doctorId,  string day)
        {
            var videoTimeIntervals = await _publicDoctorAppServices.GetVideoDoctorTimeIntervalAsync(doctorId, day);
            if (videoTimeIntervals == null || !videoTimeIntervals.Any())
            {
                return NotFound(new { response = 404, message = "No video time slots found for the given doctor on this day." });
            }

            return Ok(new { response = 200, data = videoTimeIntervals });
        }

        [HttpGet("Get_Booked_time_Slot_By_DoctId")]
        public async Task<IActionResult> GetBookedTimeSlotByDoctor([FromQuery] BookedTimeSlotByDoctorRequest request)
        {

            if(request.doct_id == 0)
            {
                return NotFound(new { Message = "No Doct found." });
            }
            var appointments = await _publicDoctorAppServices.GetBookedTimeSlot(request);
            if(appointments == null)
            {
                return Ok(appointments);
            }
            if (appointments == null || appointments.data.Count == 0)
            {
                return NotFound(new { Message = "No appointments found." });
            }

            return Ok(appointments);
        }
    }
}
