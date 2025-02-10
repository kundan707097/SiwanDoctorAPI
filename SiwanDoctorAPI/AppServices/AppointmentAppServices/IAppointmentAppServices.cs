using Abp.Application.Services;
using SiwanDoctorAPI.Model.InputDTOModel.AppointmentInputDTO;

namespace SiwanDoctorAPI.AppServices.AppointmentAppServices
{
    public interface IAppointmentAppServices : IApplicationService
    {
        Task<AppointmentResponse> CreateAppointment(AppointmentRequest request);
        Task<List<AppointmentDate>> GetAppointmentsByUserIdAsync(int userId);
        Task<AppointmentResponseByappointmentId> GetAppointmentById(int appointmentId);
        Task<UpdateStatusResponse> UpdateAppointmentStatusById(UpdateAppointmentStatus request);
        Task<AppointmentsByDateRange> GetAppointmentsByDateRange(DateTime startDate, DateTime endDate);
        Task<List<AppointmentDate>> GetAppointmentsByDoctorIdAsync(int doctorId);
    }
}
