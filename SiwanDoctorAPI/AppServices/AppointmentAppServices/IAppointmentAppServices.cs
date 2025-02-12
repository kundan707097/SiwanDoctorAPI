using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SiwanDoctorAPI.Model.InputDTOModel.AppointmentInputDTO;
using SiwanDoctorAPI.Model.InputDTOModel.TimeSlotInputDTO;

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
        Task<PagedResultDto<AppointmentDate>> GetAppointmentsByPaginationRecordss(int doctorId, int start, int end);
        Task<ServiceResponse> UpdateAppointmentStatus(int id, string status);
        Task<ServiceResponse> CancelAppointmentStatus(int id, string status);
    }
}
