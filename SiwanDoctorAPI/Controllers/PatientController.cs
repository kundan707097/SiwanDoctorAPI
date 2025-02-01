using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiwanDoctorAPI.AppServices.PatientAppServices;
using SiwanDoctorAPI.Model.InputDTOModel.UpdateInputDTO;

namespace SiwanDoctorAPI.Controllers
{
    [Authorize]
    [Route("api/auth")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientAppServices _patientAppServices;
        public PatientController(IPatientAppServices patientAppServices)
        {
                _patientAppServices = patientAppServices;
        }

        [HttpPost("update_user")]
        public async Task<IActionResult> UpdateUser([FromForm] UpdatePatientModel updateUserModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Something went to wrong");
                }
                var response = await _patientAppServices.UpdatePatientDetails(updateUserModel);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request.", error = ex.Message });
            }
        }
        [HttpGet("get_user/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            if (id <= 0)  // Validate ID
            {
                return BadRequest(new { response = 400, message = "Invalid user ID" });
            }

            var response = await _patientAppServices.GetUserByIdAsync(id);

            if (response == null || response.data == null)
            {
                return NotFound(new { response = 404, message = "User not found" });
            }

            return Ok(response);
        }

    }
}
