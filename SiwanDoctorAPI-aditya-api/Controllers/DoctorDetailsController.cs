using Microsoft.AspNetCore.Mvc;
using SiwanDoctorAPI.AppServices.DoctorAppServices;
using SiwanDoctorAPI.Model.InputDTOModel.DoctorInputDTO;

namespace SiwanDoctorAPI.Controllers
{
    [Route("api/auth")]
    public class DoctorDetailsController : ControllerBase
    {
        private readonly IDoctorAppServices _doctorAppServices;
        public DoctorDetailsController(IDoctorAppServices doctorAppServices)
        {
            _doctorAppServices = doctorAppServices;
                            
        }

        [HttpPost("update_doctor")]
        public async Task<IActionResult> UpdateDoctor([FromForm] UpdateDoctorDTO doctorDto)
        {
            var result = await _doctorAppServices.UpdateDoctorAsync(doctorDto);
            return Ok(result);
        }

        [HttpDelete("remove_doctor_image")]
        public async Task<IActionResult> RemoveDoctorImage( int id)
        {
            var result = await _doctorAppServices.RemoveDoctorImageAsync(id);
            return Ok(result);
        }

        [HttpPut("update_doctor_image")]
        public async Task<IActionResult> UpdateDoctorImage([FromForm]  DoctorUpdateImage request)
        {
            var result = await _doctorAppServices.UpdateDoctorImageAsync(request);
            return Ok(result);
        }

    }
}
