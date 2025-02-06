using Abp.Application.Services;
using SiwanDoctorAPI.Model.InputDTOModel.AppointmentInputDTO;

namespace SiwanDoctorAPI.AppServices.AppointmentAppServices
{
    public interface IAppointmentAppServices : IApplicationService
    {
        Task<AppointmentResponse> CreateAppointment(AppointmentRequest request);
        Task<List<AppointmentDate>> GetAppointmentsByUserIdAsync(int userId);
    }
}
