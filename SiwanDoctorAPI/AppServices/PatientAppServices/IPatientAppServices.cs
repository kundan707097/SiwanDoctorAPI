using Abp.Application.Services;
using SiwanDoctorAPI.Model.InputDTOModel.PatientInputDTO;
using SiwanDoctorAPI.Model.InputDTOModel.UpdateInputDTO;

namespace SiwanDoctorAPI.AppServices.PatientAppServices
{
    public interface IPatientAppServices : IApplicationService
    {
        Task<UpdatePatientResponse> UpdatePatientDetails(UpdatePatientModel updatePatientModel);
        Task<PatientResponseModel> GetUserByIdAsync(int userId);
        Task<FamilyMemberResponse> AddFamilyMemberAsync(AddFamilyMemberRequest request);
        Task<FamilyMemberResponse> AddFamilyMemberbydoctorAsync(AddFamilyMemberRequest request);
        Task<List<GetFamilyMemberResponse>> GetFamilyMembersByUserAsync(int userId);
        Task<bool> DeleteFamilyMemberByIdAsync(int id);
        Task<bool> UpdateFamilyMemberAsync(UpdateFamilyMemberDto input);
    }
}
