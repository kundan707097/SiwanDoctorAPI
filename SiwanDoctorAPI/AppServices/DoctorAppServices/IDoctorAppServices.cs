using Abp.Application.Services;
using SiwanDoctorAPI.Model.InputDTOModel.DoctorInputDTO;

namespace SiwanDoctorAPI.AppServices.DoctorAppServices
{
    public interface IDoctorAppServices : IApplicationService
    {
        Task<UpdateDoctorResponse> UpdateDoctorAsync(UpdateDoctorDTO doctorDto);
        Task<UpdateDoctorResponse> RemoveDoctorImageAsync(int id);
        Task<UpdateDoctorResponse> UpdateDoctorImageAsync(DoctorUpdateImage request);
    }
}
