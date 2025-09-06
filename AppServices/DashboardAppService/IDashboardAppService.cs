using Abp.Application.Services;
using SiwanDoctorAPI.Model.InputDTOModel.DashboardInputDTO;

namespace SiwanDoctorAPI.AppServices.DashboardAppService
{
    public interface IDashboardAppService : IApplicationService
    {
        Task<DashboardCountDto> GetDashboardCountAsync(int doctorId);
        Task<DashboardResponse> GetAdminDashboardCountAsync();
    }
}
