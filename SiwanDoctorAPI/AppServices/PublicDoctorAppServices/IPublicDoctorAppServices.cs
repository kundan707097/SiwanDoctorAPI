using Abp.Application.Services;
using SiwanDoctorAPI.Model.EntityModel.DoctorEntity;
using SiwanDoctorAPI.Model.InputDTOModel.DoctorInputDTO;
using SiwanDoctorAPI.Model.InputDTOModel.PublicInputDTO;
using SiwanDoctorAPI.Model.InputDTOModel.TimeSlotInputDTO;

namespace SiwanDoctorAPI.AppServices.PublicDoctorAppServices
{
    public interface IPublicDoctorAppServices : IApplicationService
    {
        Task<GetDoctorResponse> GetDoctorsAsync();
        Task<GetDoctorResponse> GetDoctorByIdAsync(int id);
        Task<GetDoctorResponse> GetDoctorByDepartmentIdAsync(int id);
        Task<List<ListDoctorTimeSlot>> GetDoctorTimeSlotsAsync(int doctorId);
        Task<List<DoctorTimeIntervalDTO>> GetDoctorTimeIntervalsAsync(int doctorId, string day);
        Task<List<ListDoctorTimeSlot>> GetDoctorVideoTimeSlotsAsync(int doctorId);
        Task<List<DoctorTimeIntervalDTO>> GetVideoDoctorTimeIntervalAsync(int doctorId, string day);

        Task<BookedTimeSlotByDoctorResponse> GetBookedTimeSlot(BookedTimeSlotByDoctorRequest request);
    }
}
