using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiwanDoctorAPI.AppServices.PatientAppServices;
using SiwanDoctorAPI.Model.InputDTOModel.PatientInputDTO;
using SiwanDoctorAPI.Model.InputDTOModel.UpdateInputDTO;

namespace SiwanDoctorAPI.Controllers
{
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
        public async Task<IActionResult> GetUser( int id)
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

        [HttpPost("add_family_member")]
        public async Task<IActionResult> AddFamilyMember([FromForm] AddFamilyMemberRequest request)
        {
            var response = await _patientAppServices.AddFamilyMemberAsync(request);
            return StatusCode(response.response, response);
        }
        [HttpPost("add_family_member-by-doc")]
        public async Task<IActionResult> AddFamilyMemberbydoc([FromForm] AddFamilyMemberRequest request)
        {
            var response = await _patientAppServices.AddFamilyMemberbydoctorAsync(request);
            return StatusCode(response.response, response);
        }
        [HttpGet("get_family_members/user/{userId}")]
        public async Task<IActionResult> GetFamilyMembersByUser(int userId)
        {
            var response = await _patientAppServices.GetFamilyMembersByUserAsync(userId);

            if (response == null || response.Count == 0)
            {
                return NotFound(new { status = false, message = "No family members found for this user." });
            }

            return Ok(new { status = true, message = "Family members retrieved successfully", data = response });
        }

        [HttpDelete("delete_family_member")]
        public async Task<IActionResult> DeleteFamilyMember([FromForm]int id)
        {
            var result = await _patientAppServices.DeleteFamilyMemberByIdAsync(id);

            if (!result)
                return NotFound(new { response = 404, message = "Family member not found or already deleted" });

            return Ok(new { response = 200, message = "Family member deleted successfully" });
        }
        [HttpPut("update_family_member")]
        public async Task<IActionResult> UpdateFamilyMember([FromForm] UpdateFamilyMemberDto input)
        {
            var result = await _patientAppServices.UpdateFamilyMemberAsync(input);

            if (!result)
                return NotFound(new { response = 404, message = "Family member not found" });

            return Ok(new { response = 200, message = "Family member updated successfully" });
        }

    }
}
