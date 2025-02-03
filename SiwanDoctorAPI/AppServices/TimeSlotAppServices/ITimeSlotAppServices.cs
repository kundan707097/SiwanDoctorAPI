using Abp.Application.Services;
using SiwanDoctorAPI.Model.InputDTOModel.DoctorInputDTO;
using SiwanDoctorAPI.Model.InputDTOModel.TimeSlotInputDTO;

namespace SiwanDoctorAPI.AppServices.TimeSlotAppServices
{
    public interface ITimeSlotAppServices : IApplicationService
    {
        Task<TimeSlotResponse> AddTimeSlotAsync(AddTimeSlotRequest request);
    }
}
