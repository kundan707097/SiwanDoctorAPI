using Abp.Application.Services;
using SiwanDoctorAPI.Model;
using SiwanDoctorAPI.Model.InputDTOModel.AdminInputDTO;
using SiwanDoctorAPI.Model.InputDTOModel.AppointmentInputDTO;

namespace SiwanDoctorAPI.AppServices.AdminAppServices
{
    public interface IAdminAppServices: IApplicationService
    {
        Task<List<UserDto>> GetUsersAsync();
        Task<List<AppointmentDate>> GetAppointmentsAsync(string? search, int start, int end, string? status);
        Task<List<configurations>> GetSettingsAsync();
    }
}
