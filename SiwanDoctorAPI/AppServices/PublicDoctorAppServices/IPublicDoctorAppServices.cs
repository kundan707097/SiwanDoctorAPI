using Abp.Application.Services;
using SiwanDoctorAPI.Model.InputDTOModel.DoctorInputDTO;

namespace SiwanDoctorAPI.AppServices.PublicDoctorAppServices
{
    public interface IPublicDoctorAppServices : IApplicationService
    {
        Task<GetDoctorResponse> GetDoctorsAsync();
        Task<GetDoctorResponse> GetDoctorByIdAsync(int id);
    }
}
