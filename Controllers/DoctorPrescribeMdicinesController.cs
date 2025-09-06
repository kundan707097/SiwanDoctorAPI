using Microsoft.AspNetCore.Mvc;
using SiwanDoctorAPI.AppServices.DoctorPrescribeMdicinesAppServices;
using SiwanDoctorAPI.Model.InputDTOModel.PrescribedMedicine;

namespace SiwanDoctorAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class DoctorPrescribeMdicinesController : ControllerBase
    {
        private readonly IDoctorPrescribeMdicinesAppServices _doctorPrescribeMdicines;
        public DoctorPrescribeMdicinesController(IDoctorPrescribeMdicinesAppServices doctorPrescribeMdicines)
        {
            _doctorPrescribeMdicines = doctorPrescribeMdicines;
        }

        [HttpPost("add_prescribe_medicines")]
        public async Task<IActionResult> AddPrescribedMedicine([FromForm] PrescribedMedicineRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.title) || string.IsNullOrEmpty(request.notes))
            {
                return BadRequest("Invalid data.");
            }

            var result = await _doctorPrescribeMdicines.AddPrescribedMedicineAsync(request);

            if (result > 0)
            {
                return Ok(new { response = 200, status = true, message = "Successfully", id = result });
            }
            return BadRequest("Failed to add prescribed medicine.");
        }

        [HttpGet("get_prescribe_medicines_ByDoctorId")]
        public async Task<IActionResult> GetPrescribedMedicinesByDoctorId( int doct_id)
        {
            if (doct_id <= 0)
            {
                return BadRequest("Doctor does not exist or invalid doctor ID");
            }

            var result = await _doctorPrescribeMdicines.GetMedicinesByDoctorId(doct_id);

            if (result == null || !result.Any())
            {
                return NotFound("No prescribed medicines found for this doctor.");
            }

            return Ok(new MedicineApiResponse
            {
                response = 200, // success response code
                data = result
            });
        }

        [HttpPost("update_prescribe_medicines")]
        public async Task<IActionResult> UpdatePrescribedMedicines(int id, string title,  string notes)
        {
            // Check for invalid or missing parameters
            if (id <= 0 || string.IsNullOrEmpty(title) || string.IsNullOrEmpty(notes))
            {
                return BadRequest("Invalid input data.");
            }

            // Call the service to update the medicine details
            var result = await _doctorPrescribeMdicines.UpdatePrescribedMedicine(id, title, notes);

            if (!result)
            {
                return NotFound("Medicine not found.");
            }

            // Return a successful response
            return Ok(new
            {
                response = 200,
                status = true,
                message = "successfully"
            });
        }
        [HttpPost("delete_prescribe_medicines")]
        public async Task<IActionResult> DeletePrescribedMedicine(int id)
        {
            // Check if the id is valid
            if (id <= 0)
            {
                return BadRequest("Invalid medicine ID.");
            }

            // Call the service to delete the prescribed medicine
            var result = await _doctorPrescribeMdicines.DeletePrescribedMedicine(id);

            if (!result)
            {
                return NotFound("Medicine not found.");
            }

            // Return a successful response
            return Ok(new
            {
                response = 200,
                status = true,
                message = "Medicine successfully deleted."
            });
        }

    }
}
