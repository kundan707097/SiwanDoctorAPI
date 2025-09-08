using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiwanDoctorAPI.AppServices.TimeSlotAppServices;
using SiwanDoctorAPI.Model.InputDTOModel.DoctorInputDTO;
using SiwanDoctorAPI.Model.InputDTOModel.TimeSlotInputDTO;

namespace SiwanDoctorAPI.Controllers
{
    //[Authorize]
    [Route("api/auth")]
    [ApiController]
    public class TimeSlotController : ControllerBase
    {
        private readonly ITimeSlotAppServices _timeSlotAppServices;
        public TimeSlotController(ITimeSlotAppServices timeSlotAppServices)
        {
            _timeSlotAppServices = timeSlotAppServices;


        }

        [HttpPost("add_timeslots")]
        public async Task<IActionResult> AddTimeSlot([FromForm] AddTimeSlotRequest request)
        {
            var result = await _timeSlotAppServices.AddTimeSlotAsync(request);

            // Return proper HTTP response based on the result
            return StatusCode(result.response, new
            {
                status = result.status,
                message = result.message
            });
        }

        [HttpPost("add_video_timeslots")]
        public async Task<IActionResult> AddVideoTimeSlot([FromForm] VideoTimeSlotRequest request)
        {
            var serviceResponse = await _timeSlotAppServices.AddVideoTimeSlotAsync(request);
            if (serviceResponse.response == 400 || serviceResponse.response == 500)
            {
                return BadRequest(new
                {
                    response = serviceResponse.response,
                    status = serviceResponse.status,
                    message = serviceResponse.message
                });
            }

            return StatusCode(202, new
            {
                response = serviceResponse.response,
                status = serviceResponse.status,
                message = serviceResponse.message
            });
        }

        [HttpPost("delete_videotimeslots")]
        public async Task<IActionResult> DeleteVideoTimeSlot([FromForm] DeleteVideoTimeSlotRequest request)
        {
            var serviceResponse = await _timeSlotAppServices.DeleteVideoTimeSlotAsync(request);

            if (serviceResponse.response == 400 || serviceResponse.response == 500)
            {
                return BadRequest(new
                {
                    response = serviceResponse.response,
                    status = serviceResponse.status,
                    message = serviceResponse.message
                });
            }

            return Ok(new
            {
                response = serviceResponse.response,
                status = serviceResponse.status,
                message = serviceResponse.message
            });
        }
        [HttpPost("delete_timeslots")]
        public async Task<IActionResult> DeleteTimeSlot([FromForm] DeleteTimeSlotRequest request)
        {
            var serviceResponse = await _timeSlotAppServices.DeleteTimeSlotAsync(request);
            if (serviceResponse.response == 400 || serviceResponse.response == 500)
            {
                return BadRequest(new
                {
                    response = serviceResponse.response,
                    status = serviceResponse.status,
                    message = serviceResponse.message
                });
            }

            return Ok(new
            {
                response = serviceResponse.response,
                status = serviceResponse.status,
                message = serviceResponse.message
            });
        }
    }
}
