using Azure;
using Microsoft.AspNetCore.Mvc;
using SiwanDoctorAPI.AppServices.FamilyVitalsAppServices;
using SiwanDoctorAPI.Model.InputDTOModel.FamilyMemberVitalsInoutDTO;

namespace SiwanDoctorAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class FamilyVitalsController : ControllerBase
    {
        private readonly IFamilyVitalsAppServices _familyVitalsAppServices;
        public FamilyVitalsController(IFamilyVitalsAppServices familyVitalsAppServices)
        {
           _familyVitalsAppServices = familyVitalsAppServices;
        }

        [HttpPost("add_vitals")]
        public async Task<IActionResult> AddVitals([FromForm] AddVitalsInputModel input)
        {
            if (input == null)
            {
                return BadRequest(new
                {
                    response = 400,
                    status = false,
                    message = "Invalid input"
                });
            }

            var result = await _familyVitalsAppServices.AddVitalsAsync(input);

            if (result)
            {
                return Ok(new
                {
                    response = 200,
                    status = true,
                    message = "successfully"
                });
            }

            return BadRequest(new
            {
                response = 400,
                status = false,
                message = "Failed to add vitals"
            });
        }

        [HttpDelete("delete_vitals")]
        public async Task<IActionResult> DeleteVitals([FromForm] int id)
        {
            bool isDeleted = await _familyVitalsAppServices.DeleteVitalAsync(id);

            if (!isDeleted)
                return NotFound(new { message = "Vital record not found or invalid ID." });

            return Ok(new { message = "Vital record deleted successfully." });
        }

        [HttpGet("get_vitals")]
        public async Task<IActionResult> GetVitals()
        {
            var vitals = await _familyVitalsAppServices.GetAllVitalsAsync();

            if (vitals == null || !vitals.Any())
                return NotFound(new { response = 404, message = "No vital records found." });

            return Ok(new { response = 200, data = vitals });
        }

        [HttpGet("get_vitals/{id}")]
        public async Task<IActionResult> GetVitals([FromForm] int id)
        {
            var vitals = await _familyVitalsAppServices.GetVitalsByIdAsync(id);

            if (vitals == null)
            {
                return NotFound(new { response = 404, message = "Vitals not found" });
            }

            return Ok(new
            {
                response = 200,
                data = vitals
            });
        }

        [HttpGet("get_vitals/user/{userId}")]
        public async Task<IActionResult> GetVitalsByUser([FromForm] int userId)
        {
            // Fetch vitals data for the user from the service
            var vitals = await _familyVitalsAppServices.GetVitalsByUserIdAsync(userId);

            if (vitals == null || vitals.Count == 0)
            {
                return NotFound(new { response = 404, message = "No vitals found for this user" });
            }

            // Prepare the response
            var response = new VitalRecordsResponse
            {
                Response = 200,
                Data = vitals
            };

            return Ok(response);
        }


        [HttpGet("get_vitals/family_member/{familyMemberId}")]
        public async Task<IActionResult> GetVitalsByFamilyMember([FromForm] int familyMemberId)
        {
            // Fetch vitals data for the family member from the service
            var vitals = await _familyVitalsAppServices.GetVitalsByFamilyMemberIdAsync(familyMemberId);

            if (vitals == null || vitals.Count == 0)
            {
                return NotFound(new { message = "No vitals found for this family member" });
            }

            // Prepare the response object
            var response = new VitalRecordsResponse
            {
                Response = 200,
                Data = vitals
            };

            return Ok(response);
        }

        [HttpGet("get_vitals_family_member_id_type")]
        public async Task<ActionResult> GetVitalsByFamilyMemberAndType(
            int familyMemberId,
            string type,
             DateTime startDate,
             DateTime endDate)
        {
            var vitals = await _familyVitalsAppServices.GetVitalsByFamilyMemberAndTypeAsync(familyMemberId, type, startDate, endDate);
            if (vitals == null || vitals.Count==0)
            {
                return NotFound(new { message = "No vitals found for this family member this type" });
            }
            var response = new VitalRecordsResponse
            {
                Response = 200,
                Data = vitals
            };
            return Ok(response);
        }

        [HttpPost("update_vitals")]
        public async Task<IActionResult> UpdateVitals([FromForm] UpdateVitalsRequestInputDTO request)
        {
            if (request == null || request.id == 0)
            {
                return BadRequest("Invalid input data.");
            }

            var success = await _familyVitalsAppServices.UpdateVitalsAsync(request);

            if (success)
            {
                return Ok(new { response = 200, message = "Vitals updated successfully." });
            }
            else
            {
                return NotFound(new { response = 404, message = "Vital record not found." });
            }
        }

    }
}
