using Microsoft.AspNetCore.Mvc;
using SiwanDoctorAPI.AppServices.VideoMettingAppServices;

namespace SiwanDoctorAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class VideoMettingController : ControllerBase
    {
        private readonly IVideoMettingAppServices _videoMettingAppServices;
        public VideoMettingController(IVideoMettingAppServices videoMettingAppServices)
        {

            _videoMettingAppServices = videoMettingAppServices;
        }
        //[HttpPost("create-meeting")]
        //public async Task<IActionResult> CreateMeeting()
        //{
        //    var meetingDetails = await _videoMettingAppServices.CreateMeeting();
        //    return Ok(meetingDetails);
        //}
    }
}
