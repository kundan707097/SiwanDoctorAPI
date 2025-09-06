using Abp.Application.Services;
using SiwanDoctorAPI.Model.EntityModel.SettingEntity;

namespace SiwanDoctorAPI.AppServices.WebPageAppServices
{
    public interface ISettingAppServices : IApplicationService
    {
        Task<List<WebPage>> GetAllWebPagesAsync();
    }
}
