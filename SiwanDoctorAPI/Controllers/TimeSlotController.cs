using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiwanDoctorAPI.AppServices.TimeSlotAppServices;
using SiwanDoctorAPI.Model.InputDTOModel.DoctorInputDTO;

namespace SiwanDoctorAPI.Controllers
{
    //[Authorize]
    [Route("api/auth")]
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
    }
}
