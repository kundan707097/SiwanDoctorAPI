using Abp.Application.Services;
using SiwanDoctorAPI.Model.InputDTOModel.DoctorInputDTO;
using SiwanDoctorAPI.Model.InputDTOModel.TimeSlotInputDTO;

namespace SiwanDoctorAPI.AppServices.TimeSlotAppServices
{
    public interface ITimeSlotAppServices : IApplicationService
    {
        Task<TimeSlotResponse> AddTimeSlotAsync(AddTimeSlotRequest request);
        Task<ServiceResponse> AddVideoTimeSlotAsync(VideoTimeSlotRequest request);
        Task<ServiceResponse> DeleteVideoTimeSlotAsync(DeleteVideoTimeSlotRequest request);
        Task<ServiceResponse> DeleteTimeSlotAsync(DeleteTimeSlotRequest request);
    }
}
