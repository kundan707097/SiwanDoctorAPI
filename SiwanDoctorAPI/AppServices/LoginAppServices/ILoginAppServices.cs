using Abp.Application.Services;
using SiwanDoctorAPI.Model.InputDTOModel.LoginInputDTO;
using SiwanDoctorAPI.Model.InputDTOModel.PatientInputDTO;
using SiwanDoctorAPI.Model.InputDTOModel.RegistrationInputDTO;

namespace SiwanDoctorAPI.AppServices.LoginAppServices
{
    public interface ILoginAppServices : IApplicationService
    {
        Task<LoginResponse> LoginAsync(LoginInput loginRequest);
        Task<LoginResponse> LoginWithPhoneNumber(string phoneNumber);
        Task<PatientUpdatePassowardResponse> UpdatePatientPassword(PatientUpdatePasswordModel model);
        Task<RegistrationOutputResponse> UpdateUserRoleAsync(string userId, string newRole);
    }
}
