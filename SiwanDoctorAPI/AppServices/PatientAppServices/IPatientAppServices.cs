using Abp.Application.Services;
using SiwanDoctorAPI.Model.InputDTOModel.PatientInputDTO;
using SiwanDoctorAPI.Model.InputDTOModel.UpdateInputDTO;

namespace SiwanDoctorAPI.AppServices.PatientAppServices
{
    public interface IPatientAppServices : IApplicationService
    {
        Task<UpdatePatientResponse> UpdatePatientDetails(UpdatePatientModel updatePatientModel);
        Task<PatientResponseModel> GetUserByIdAsync(int userId);
    }
}
