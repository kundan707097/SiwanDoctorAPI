using Microsoft.AspNetCore.Mvc;
using SiwanDoctorAPI.AppServices.PatientPrescriptionAppServices;
using SiwanDoctorAPI.Model.InputDTOModel.PrescribedMedicine;

namespace SiwanDoctorAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class PatientPrescriptionController : ControllerBase
    {
        public readonly IPatientPrescriptionAppServices _patientPrescriptionAppServices;
        public PatientPrescriptionController(IPatientPrescriptionAppServices patientPrescriptionAppServices)
        {

            _patientPrescriptionAppServices = patientPrescriptionAppServices;
        }

        [HttpPost("add_data")]
        public async Task<IActionResult> AddPrescription([FromForm] PrescriptionDto prescriptionDto)
        {
            if (prescriptionDto == null)
            {
                return BadRequest(new
                {
                    response = 400,
                    status = false,
                    message = "Invalid prescription data.",
                    id = 0
                });
            }

            // Call the service method to add prescription
            int prescriptionId = await _patientPrescriptionAppServices.AddPrescriptionAsync(prescriptionDto);
            if (prescriptionId == 0)
            {
                return BadRequest(new
                {
                    response = 400,
                    status = false,
                    message = "Failed to save prescription. Patient, Doctor, or Appointment not found.",
                    id = 0
                });
            }

            return Ok(new
            {
                response = 200,
                status = true,
                message = "Successfully",
                id = prescriptionId
            });
        }

        [HttpGet("get_prescription")]
        public async Task<IActionResult> GetPrescriptions()
        {
            var prescriptions = await _patientPrescriptionAppServices.GetPrescriptionsAsync();

            return Ok(new
            {
                response = 200,
                data = prescriptions
            });
        }
        [HttpGet("get_prescription_by_prescriptionId")]
        public async Task<IActionResult> GetPrescription(int prescriptionId)
        {
            var prescription = await _patientPrescriptionAppServices.GetPrescriptionByIdAsync(prescriptionId);
            if (prescription == null)
            {
                return NotFound(new { response = 404, message = "Prescription not found" });
            }

            return Ok(new { response = 200, data = prescription });
        }

        [HttpGet("Get_Data_By_Appointment_Id")]
        public async Task<IActionResult> GetPrescriptionByAppointmentId(int appointmentId)
        {
            var prescriptions = await _patientPrescriptionAppServices.GetPrescriptionsByAppointmentIdAsync(appointmentId);

            if (prescriptions == null || prescriptions.Count == 0)
            {
                return NotFound(new { response = 404, message = "No prescriptions found for this appointment" });
            }

            return Ok(new { response = 200, data = prescriptions });
        }

        [HttpPost("delete_prescription")]
        public async Task<IActionResult> DeletePrescription(int id)
        {
            var result = await _patientPrescriptionAppServices.DeletePrescriptionAsync(id);
            return StatusCode(result.response, result);
        }

        [HttpPost("update_prescription")]
        public async Task<IActionResult> UpdatePrescription([FromForm] UpdatePrescriptionDto request)
        {
            var result = await _patientPrescriptionAppServices.UpdatePrescriptionAsync(request);
            return StatusCode(result.response, result);
        }

        [HttpGet("get_prescription/doctor/{doctorId}")]
        public async Task<IActionResult> GetPrescriptionsByDoctorId(int doctorId)
        {
            var response = await _patientPrescriptionAppServices.GetPrescriptionsByDoctorAsync(doctorId);
            return Ok(new
            {
                response = 200,
                data = response
            });
        }
        [HttpGet("get_prescription/User/{userId}")]
        public async Task<IActionResult> GetPatientPrescriptionsByUserId(int userId)
        {
            var response = await _patientPrescriptionAppServices.GetPatientPrescriptionsByUserIdAsync(userId);
            return Ok( new
            {
                response = 200,
                data = response
            });
        }
        [HttpGet("get_prescription/Patient/{PatientId}")]
        public async Task<IActionResult> GetPatientPrescriptionsByPatientId(int PatientId)
        {
            var response = await _patientPrescriptionAppServices.GetPatientPrescriptionsByPatientIdAsync(PatientId);
            return Ok(new
            {
                response = 200,
                data = response
            });
        }
    }
}
