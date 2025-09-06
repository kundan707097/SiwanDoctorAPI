using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiwanDoctorAPI.AppServices.SocialMediaAppservices;
using SiwanDoctorAPI.Model.InputDTOModel.SocialMediaInputDTO;

namespace SiwanDoctorAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class SocialMediaController : ControllerBase
    {
        private readonly ISocialMediaAppservices _socialMediaAppservices;
        public SocialMediaController(ISocialMediaAppservices socialMediaAppservices)
        {
            
            _socialMediaAppservices = socialMediaAppservices;
        }
        [HttpPost("add_social_media")]
        public async Task<IActionResult> AddSocialMedia([FromForm] SocialMediaRequest request)
        {
            var result = await _socialMediaAppservices.AddSocialMediaAsync(request);
            return Ok(result);
        }

        [HttpPost("update_social_media")]
        public async Task<IActionResult> UpdateSocialMedia([FromForm] UpdateSocialMediaRequest request)
        {
            var result = await _socialMediaAppservices.UpdateSocialMediaAsync(request);
            return Ok(result);
        }
        [HttpPost("remove_social_media_image")]
        public async Task<IActionResult> RemoveSocialMediaImage([FromForm] RemoveSocialMediaImage request)
        {
            var result = await _socialMediaAppservices.RemoveSocialMediaImageAsync(request);
            return Ok(result);
        }
        [HttpDelete("delete_social_media")]
        public async Task<IActionResult> DeleteSocialMedia([FromForm] DeleteSocialMedia request)
        {
            var result = await _socialMediaAppservices.DeleteSocialMedia(request);
            return Ok(result);
        }
        [HttpGet("get_social_media")]
        public async Task<IActionResult> GetSocialMedia()
        {
            var result = await _socialMediaAppservices.GetSocialMediaAsync();
            return Ok(new { response = 200, data = result });
        }
    }
}
