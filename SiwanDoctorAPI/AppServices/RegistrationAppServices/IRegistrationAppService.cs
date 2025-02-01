using Abp.Application.Services;
using SiwanDoctorAPI.Model.InputDTOModel.RegistrationInputDTO;

namespace SiwanDoctorAPI.AppServices.RegistrationAppServices
{
    public interface IRegistrationAppService : IApplicationService
    {
        Task<RegistrationOutputResponse> PatientsAndDoctorRegistration(RegisterModel registerModel);
    }
}
