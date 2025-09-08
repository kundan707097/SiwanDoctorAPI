using Abp.Application.Services;
using SiwanDoctorAPI.Model.InputDTOModel.PrescribedMedicine;

namespace SiwanDoctorAPI.AppServices.PatientPrescriptionAppServices
{
    public interface IPatientPrescriptionAppServices: IApplicationService
    {
        Task<int> AddPrescriptionAsync(PrescriptionDto prescriptionDto);
        Task<List<GetPrescriptionDto>> GetPrescriptionsAsync();
        Task<GetPrescriptionDto> GetPrescriptionByIdAsync(int prescriptionId);
        Task<List<GetPrescriptionDto>> GetPrescriptionsByAppointmentIdAsync(int appointmentId);
        Task<ApiResponse> DeletePrescriptionAsync(int id);
        Task<ApiResponse> UpdatePrescriptionAsync(UpdatePrescriptionDto request);
        Task<List<GetPrescriptionDto>> GetPrescriptionsByDoctorAsync(int doctorId);
        Task<List<GetPrescriptionDto>> GetPatientPrescriptionsByUserIdAsync(int userId);
        Task<List<GetPrescriptionDto>> GetPatientPrescriptionsByPatientIdAsync(int PatientId);
    }
}
