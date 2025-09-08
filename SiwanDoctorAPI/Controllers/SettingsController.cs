using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiwanDoctorAPI.AppServices.AdminAppServices;
using SiwanDoctorAPI.DbConnection;
using SiwanDoctorAPI.Model;
using SiwanDoctorAPI.Model.InputDTOModel.PatientInputDTO;

namespace SiwanDoctorAPI.Controllers
{
    [Route("api/v1/get_configurations")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly IAdminAppServices _adminAppServices;
        private readonly ApplicationDbContext _context;

        public SettingsController(IAdminAppServices adminAppServices,ApplicationDbContext context)
        {
            _adminAppServices = adminAppServices;
            _context = context;
        }

        // GET: api/Settings
        [HttpGet]
        public async Task<ActionResult> get_configurations()
        {
            try
            {
                var settings = await _context.configurations
                    .Select(c => new
                    {
                        id = c.Id,
                        id_name = c.IdName,
                        group_name = c.GroupName,
                        preferences = c.Preferences,
                        title = c.Title,
                        value = c.IdName == "zoom_client_id" || c.IdName == "zoom_client_secret" ||
                                c.IdName == "zoom_account_id" || c.IdName == "google_service_account_json"
                                ? "[REDACTED]" : c.Value, // Redact sensitive data
                        description = c.Description,
                        created_at = c.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
                        updated_at = c.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss")
                    })
                    .ToListAsync();

                return Ok(new
                {
                    response = 200,
                    data = settings
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    response = 500,
                    message = "Error retrieving configurations"
                });
            }
        }


    }

    // DTO Model
    public class ConfigurationsDto
    {
        public int Id { get; set; }
        public string IdName { get; set; }
        public string GroupName { get; set; }
        public int Preferences { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
  
}
