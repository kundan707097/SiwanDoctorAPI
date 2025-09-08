using Abp.Application.Services;

namespace SiwanDoctorAPI.AppServices.VideoMettingAppServices
{
    public interface IVideoMettingAppServices: IApplicationService
    {
        Task<string> CreateMeeting(string doctorEmail, string doctorname);
    }
}
