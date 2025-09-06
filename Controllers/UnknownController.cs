using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiwanDoctorAPI.AppServices.DepartmentAppServices;
using SiwanDoctorAPI.AppServices.DoctorReviewAppServices;
using SiwanDoctorAPI.AppServices.PublicDoctorAppServices;
using SiwanDoctorAPI.DbConnection;

namespace SiwanDoctorAPI.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class UnknownController : ControllerBase
    {
        private readonly IDepartmentAppServices _departmentAppServices;
        private readonly ApplicationDbContext _context;
        private readonly IPublicDoctorAppServices _publicDoctorAppServices;
        private readonly IDoctorReviewAppServices _doctorReviewAppServices;

        public UnknownController(ApplicationDbContext context, IDepartmentAppServices departmentAppServices, IPublicDoctorAppServices publicDoctorAppServices, IDoctorReviewAppServices doctorReviewAppServices)
        {
            _context = context;
            _departmentAppServices = departmentAppServices;
            _publicDoctorAppServices = publicDoctorAppServices;
            _doctorReviewAppServices = doctorReviewAppServices;
        }
        
       


        // GET: api/unknown/testimonials
        [HttpGet("get_testimonial")]
        public async Task<IActionResult> GetTestimonials()
        {
            var testimonials = await _context.GetTestimonal.ToListAsync();
            return Ok(new
            {
                response = 200,
                data = testimonials
            });
        }
        [HttpGet("get_social_media")]
        public async Task<IActionResult> GetSocialMedia()
        {
       
                var socialMediaLinks = await _context.GetSocialMedia.ToListAsync();
                return Ok(new
                {
                    response = 200,
                    data = socialMediaLinks
                });
            }
        // GET: api/v1/get_web_page/page/{id}
        [HttpGet("get_web_page/page/{id}")]
        public async Task<IActionResult> GetWebPageByPageId(int id)
        {
            var page = await _context.webPages
                .Where(p => p.PageId == id && !p.IsDeleted)
                .FirstOrDefaultAsync();

            if (page == null)
            {
                return NotFound(new
                {
                    response = 404,
                    message = "Page not found"
                });
            }

            return Ok(new
            {
                response = 200,
                data = page
            });
        }
        [HttpGet("get_department_active")]
        public async Task<IActionResult> GetDepartmentsActive()
        {
            var response = await _departmentAppServices.GetActiveDepartmentsAsync();
            return Ok(response);
        }

        [HttpGet("get_doctor_dep_id/{id}")]
        public async Task<IActionResult> get_doctor_dep_id(int id)
        {
            var result = await _publicDoctorAppServices.GetDoctorByDepartmentIdAsync(id);

            if (result == null)
                return NotFound(new { response = 404, message = "Doctor not found" });

            return Ok(result);
        }

        [HttpGet("get_doctor_review/doctor/{id}")]
        public async Task<IActionResult> get_doctor_review(int id)
        {
            var reviews = await _doctorReviewAppServices.GetByIdReviewsAsync(id);
            return Ok(new { response = 200, data = reviews });
        }
    }
}
