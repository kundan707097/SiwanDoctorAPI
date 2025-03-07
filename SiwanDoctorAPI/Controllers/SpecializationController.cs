using Microsoft.AspNetCore.Mvc;
using SiwanDoctorAPI.AppServices.SpecializationAppServices;
using SiwanDoctorAPI.Model.InputDTOModel.SpecializationInputDTO;

namespace SiwanDoctorAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        private readonly ISpecializationAppServices _specializationService;
        public SpecializationController(ISpecializationAppServices specializationService)
        {
            _specializationService = specializationService;
        }

        [HttpPost("add_specialization")]
        public async Task<IActionResult> AddSpecialization([FromForm] SpecializationDto specializationDto)
        {
            var specializationId = await _specializationService.AddSpecializationAsync(specializationDto);

            if (specializationId > 0)
            {
                return Ok(new
                {
                    response = 200,
                    status = true,
                    message = "Successfully",
                    id = specializationId
                });
            }

            return BadRequest(new
            {
                response = 400,
                status = false,
                message = "Error adding specialization"
            });
        }

        [HttpPost("update_specialization")]
        public async Task<IActionResult> UpdateSpecialization([FromForm] UpdateSpecializationInputDTO request)
        {
            var isUpdated = await _specializationService.UpdateSpecializationAsync(request);

            if (!isUpdated)
                return NotFound(new { response = 404, message = "Specialization not found" });

            return Ok(new { response = 200, message = "Specialization updated successfully" });
        }

        [HttpPost("delete_specialization")]
        public async Task<IActionResult> DeleteSpecialization([FromForm] DeleteSpecializationDTO request)
        {
            var isDeleted = await _specializationService.SoftDeleteSpecializationAsync(request.id);

            if (!isDeleted)
                return NotFound(new { response = 404, message = "Specialization not found" });

            return Ok(new { response = 200, message = "Specialization deleted successfully" });
        }

        [HttpGet("get_specialization")]
        public async Task<IActionResult> GetSpecializations()
        {
            var specializations = await _specializationService.GetSpecializationsAsync();

            if (specializations == null || !specializations.Any())
                return NotFound(new { response = 404, message = "No specializations found" });

            return Ok(new { response = 200, data = specializations });
        }

        [HttpGet("get_specialization/{id}")]
        public async Task<IActionResult> GetSpecializationById(int id)
        {
            var specialization = await _specializationService.GetSpecializationByIdAsync(id);

            if (specialization == null)
                return NotFound(new { response = 404, message = "Specialization not found" });

            return Ok(new { response = 200, data = specialization });
        }

    }
}
