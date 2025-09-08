using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiwanDoctorAPI.AppServices.DoctorReviewAppServices;
using SiwanDoctorAPI.Model.InputDTOModel.DoctorReview;

namespace SiwanDoctorAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class DoctorReviewController : ControllerBase
    {
        private readonly IDoctorReviewAppServices _doctorReviewAppServices;
        public DoctorReviewController(IDoctorReviewAppServices doctorReviewAppServices)
        {
            
            _doctorReviewAppServices = doctorReviewAppServices;
        }


        [HttpPost("add_doctor_review")]
        public async Task<IActionResult> AddDoctorReview([FromForm] AddDoctorReviewRequest request)
        {
            var response = await _doctorReviewAppServices.AddDoctorReviewAsync(request);
            return Ok(response);
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetDoctorReviews(int doctorId)
        {
            var response = await _doctorReviewAppServices.GetDoctorReviewsAsync(doctorId);
            return Ok(response);
        }

        [HttpGet("get_all_doctor_review")]
        public async Task<IActionResult> GetAllDoctorReviews()
        {
            var reviews = await _doctorReviewAppServices.GetAllReviewsAsync();
            return Ok(new { response = 200, data = reviews });
        }

    
    }
}
