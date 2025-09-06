using Abp.Application.Services;
using SiwanDoctorAPI.Model.InputDTOModel.SocialMediaInputDTO;

namespace SiwanDoctorAPI.AppServices.SocialMediaAppservices
{
    public interface ISocialMediaAppservices: IApplicationService
    {
        Task<SocialMediaResponse> AddSocialMediaAsync(SocialMediaRequest request);
        Task<UpdateSocialMediaResponse> UpdateSocialMediaAsync(UpdateSocialMediaRequest request);

        Task<UpdateSocialMediaResponse> RemoveSocialMediaImageAsync(RemoveSocialMediaImage request);
        Task<UpdateSocialMediaResponse> DeleteSocialMedia(DeleteSocialMedia request);
        Task<List<SocialMediaDto>> GetSocialMediaAsync();
    }
}
