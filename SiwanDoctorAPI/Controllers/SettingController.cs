using Microsoft.AspNetCore.Mvc;
using SiwanDoctorAPI.AppServices.WebPageAppServices;

namespace SiwanDoctorAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly ISettingAppServices _settingAppServices;
        public SettingController(ISettingAppServices settingAppServices)
        {
            _settingAppServices = settingAppServices;
        }

        [HttpGet("get_web_pages")]
        public async Task<IActionResult> GetWebPages()
        {
            var webPages = await _settingAppServices.GetAllWebPagesAsync();

            var response = new
            {
                response = 200,
                data = webPages
            };

            return Ok(response);
        }
    }
}
