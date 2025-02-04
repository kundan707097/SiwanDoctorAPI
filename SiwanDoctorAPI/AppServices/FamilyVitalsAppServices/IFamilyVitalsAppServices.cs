using Abp.Application.Services;
using SiwanDoctorAPI.Model.InputDTOModel.FamilyMemberVitalsInoutDTO;

namespace SiwanDoctorAPI.AppServices.FamilyVitalsAppServices
{
    public interface IFamilyVitalsAppServices : IApplicationService
    {
        Task<bool> AddVitalsAsync(AddVitalsInputModel input);
        Task<bool> DeleteVitalAsync(int id);
        Task<List<object>> GetAllVitalsAsync();
        Task<GetVitalsModel> GetVitalsByIdAsync(int id);
        Task<List<VitalRecord>> GetVitalsByUserIdAsync(int userId);
        Task<List<VitalRecord>> GetVitalsByFamilyMemberIdAsync(int familyMemberId);
        Task<List<VitalRecord>> GetVitalsByFamilyMemberAndTypeAsync(int familyMemberId, string type, DateTime startDate, DateTime endDate);
        Task<bool> UpdateVitalsAsync(UpdateVitalsRequestInputDTO request);
    }
}
