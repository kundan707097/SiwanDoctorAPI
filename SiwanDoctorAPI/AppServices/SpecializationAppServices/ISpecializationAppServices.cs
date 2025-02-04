using Abp.Application.Services;
using SiwanDoctorAPI.Model.InputDTOModel.SpecializationInputDTO;

namespace SiwanDoctorAPI.AppServices.SpecializationAppServices
{
    public interface ISpecializationAppServices : IApplicationService
    {
        Task<int> AddSpecializationAsync(SpecializationDto specializationDto);
        Task<bool> UpdateSpecializationAsync(UpdateSpecializationInputDTO request);
        Task<bool> SoftDeleteSpecializationAsync(int id);
        Task<List<GetAllSpecializationResponse>> GetSpecializationsAsync();
        Task<GetAllSpecializationResponse> GetSpecializationByIdAsync(int id);
    }
}
