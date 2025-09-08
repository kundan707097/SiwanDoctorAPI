using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiwanDoctorAPI.AppServices.LoginAppServices;
using SiwanDoctorAPI.AppServices.RegistrationAppServices;
using SiwanDoctorAPI.DbConnection;
using SiwanDoctorAPI.Model.InputDTOModel.LoginInputDTO;
using SiwanDoctorAPI.Model.InputDTOModel.PatientInputDTO;
using SiwanDoctorAPI.Model.InputDTOModel.RegistrationInputDTO;
using SiwanDoctorAPI.Model.InputDTOModel.UpdateInputDTO;

namespace SiwanDoctorAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILoginAppServices _loginAppServices;
        private readonly IRegistrationAppService _registrationAppService;
        private readonly ApplicationDbContext _context;
        public AccountController(
            ILoginAppServices loginAppServices, IRegistrationAppService registrationAppService,ApplicationDbContext context)
        {
            _loginAppServices = loginAppServices;
            _registrationAppService = registrationAppService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid registration request.");
            }

            var result = await _registrationAppService.PatientsAndDoctorRegistration(model);

            if (result.status)
            {
                return Ok(result);
            }

            return StatusCode(result.response, result.message);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] LoginInput loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid login request");
            }

            var response = await _loginAppServices.LoginAsync(loginRequest);

            if (!response.status)
            {
                return Unauthorized(new { response.message });
            }

            return Ok(response);
        }

        [HttpPost("login_phone")]
        public async Task<IActionResult> LoginWithPhone([FromForm] LoginRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Phone))
                {
                    return BadRequest(new { message = "Phone number is required." });
                }

                var response = await _loginAppServices.LoginWithPhoneNumber(request.Phone);

                if (!response.status)
                {
                    return Unauthorized(new { response.message });
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request.", error = ex.Message });
            }
        }



        [HttpPost("update_password")]
        public async Task<IActionResult> UpdatePassword([FromForm] PatientUpdatePasswordModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.user_id) || string.IsNullOrEmpty(model.password))
            {
                return BadRequest(new { response = 400, message = "Invalid request data" });
            }

            var response = await _loginAppServices.UpdatePatientPassword(model);


            return Ok(response);
        }
        [HttpPost("update-role")]
        public async Task<IActionResult> UpdateUserRole([FromForm] UpdateUserRoleRequest model)
        {
            if (string.IsNullOrEmpty(model.UserId) || string.IsNullOrEmpty(model.NewRole))
            {
                return BadRequest(new { message = "UserId and NewRole are required." });
            }

            var result = await _loginAppServices.UpdateUserRoleAsync(model.UserId, model.NewRole);

            if (result.status)
            {
                return Ok(result);
            }
            return StatusCode(result.response, result);
        }

        [HttpGet("checkPhoneNumberExist/{number}")]
       
        public async Task<IActionResult> CheckPhoneNumberExist(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                return BadRequest(new { message = "Phone number is required" });
            }

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == number);
            if (existingUser != null)
            {
                return Conflict(new { message = "Phone number already exists" });
            }

            return Ok(new { message = "Phone number is available" });
        }

        [HttpGet("get_user{id}")]
        public async Task<IActionResult> get_user(int id)
        {
            var ui = await _context.Users.FirstOrDefaultAsync(i => i.Id == id);
            return Ok(new { response = 200, data = ui });
        }
    }

}
