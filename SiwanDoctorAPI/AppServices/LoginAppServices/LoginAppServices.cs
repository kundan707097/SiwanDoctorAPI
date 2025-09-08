using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SiwanDoctorAPI.DbConnection;
using SiwanDoctorAPI.Model.InputDTOModel.LoginInputDTO;
using SiwanDoctorAPI.Model.InputDTOModel.PatientInputDTO;
using SiwanDoctorAPI.Model.InputDTOModel.RegistrationInputDTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static SiwanDoctorAPI.DbConnection.ApplicationDbContext;

namespace SiwanDoctorAPI.AppServices.LoginAppServices
{
    public class LoginAppServices : ILoginAppServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _applicationDbContext;

        public LoginAppServices(UserManager<ApplicationUser> userManager, IConfiguration configuration, ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        public async Task<LoginResponse> LoginAsync(LoginInput loginRequest)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == loginRequest.emailOrPhoneNumber);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginRequest.password))
            {
                return new LoginResponse
                {
                    response = 401,
                    status = false,
                    message = "Invalid username or password"
                };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = GenerateToken(user, roles);

            var role = roles.FirstOrDefault();
            UserData userDetails = null;

            if (role == "Doctor")
            {
                var doctor = await _applicationDbContext.Doctor_Details
                    .Where(d => d.UserId == user.Id)
                    .FirstOrDefaultAsync();

                if (doctor != null)
                {
                    userDetails = new UserData
                    {
                        id = doctor.Id,
                        f_name = doctor.FirstName ?? string.Empty,
                        l_name = doctor.LastName ?? string.Empty,
                        phone = doctor.Phone ?? string.Empty,
                        isd_code = doctor.ISDCode ?? string.Empty,
                        gender = doctor.Gender ?? string.Empty,
                        dob = doctor.DateOfBirth?.ToString("yyyy-MM-dd") ?? string.Empty,
                        email = user.Email ?? string.Empty,
                        address = doctor.doctor_Address ?? string.Empty,
                        city = string.Empty,
                        state = string.Empty,
                        postal_code = int.TryParse(doctor.PostalCode, out int postalCode) ? postalCode : 0,
                        created_at = doctor.CreationTime.ToString("yyyy-MM-ddTHH:mm:ss.ffffffZ"),
                        updated_at = doctor.LastModificationTime?.ToString("yyyy-MM-ddTHH:mm:ss.ffffffZ") ?? string.Empty,
                        role = roles.Select(r => new UserRole
                        {
                            id = doctor.Id,
                            user_id = user.Id,
                            name = r
                        }).ToList()
                    };
                }
            }
            else if (role == "Patient")
            {
                var patient = await _applicationDbContext.Patients_Details
                    .Where(p => p.UserId == user.Id)
                    .FirstOrDefaultAsync();

                if (patient != null)
                {
                    userDetails = new UserData
                    {
                        id = patient.Id,
                        f_name = patient.FirstName ?? string.Empty,
                        l_name = patient.LastName ?? string.Empty,
                        phone = user.PhoneNumber ?? string.Empty,
                        isd_code = "+91",
                        gender = patient.Gender ?? string.Empty,
                        dob = patient.DateOfBirth?.ToString("yyyy-MM-dd") ?? string.Empty,
                        email = user.Email ?? string.Empty,
                        address = patient.patient_Address ?? string.Empty,
                        city = patient.city ?? string.Empty,
                        state = patient.State ?? string.Empty,
                        postal_code = int.TryParse(patient.PinCode, out int postalCode) ? postalCode : 0,
                        created_at = patient.CreationTime.ToString("yyyy-MM-ddTHH:mm:ss.ffffffZ"),
                        updated_at = patient.LastModificationTime?.ToString("yyyy-MM-ddTHH:mm:ss.ffffffZ") ?? string.Empty,
                        role = roles.Select(r => new UserRole
                        {
                            id = patient.Id,
                            user_id = user.Id,
                            name = r
                        }).ToList()
                    };
                }
            }

            if (userDetails == null)
            {
                return new LoginResponse
                {
                    response = 404,
                    status = false,
                    message = "User details not found"
                };
            }

            return new LoginResponse
            {
                response = 200,
                status = true,
                message = "Successfully",
                data = userDetails,
                token = token
            };
        }

        private string GenerateToken(ApplicationUser user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public async Task<LoginResponse> LoginWithPhoneNumber(string phoneNumber)
        {
            try
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
                if (user == null)
                {
                    return new LoginResponse
                    {
                        response = 201,
                        status = false,
                        message = "Mobile Number Does not exist"
                    };
                }
                var roles = await _userManager.GetRolesAsync(user);
                var token = GenerateToken(user, roles);
                
                var role = roles.FirstOrDefault();
                UserData userDetails = null;
                if (user.UserType == SiwanDoctorAPI.DbConnection.ApplicationDbContext.UserType.Doctor)

                {
                    var doctor = await _applicationDbContext.Doctor_Details
                        .Where(d => d.UserId == user.Id)
                        .FirstOrDefaultAsync();

                    if (doctor != null)
                    {
                        userDetails = new UserData
                        {
                            id = doctor.Id,
                            f_name = doctor.FirstName ?? string.Empty,
                            l_name = doctor.LastName ?? string.Empty,
                            phone = doctor.Phone ?? string.Empty,
                            isd_code = doctor.ISDCode ?? string.Empty,
                            gender = doctor.Gender ?? string.Empty,
                            dob = doctor.DateOfBirth?.ToString("yyyy-MM-dd") ?? string.Empty,
                            email = user.Email ?? string.Empty,
                            address = doctor.doctor_Address ?? string.Empty,
                            city = string.Empty,
                            state = string.Empty,
                            postal_code = int.TryParse(doctor.PostalCode, out int postalCode) ? postalCode : 0,
                            created_at = doctor.CreationTime.ToString("yyyy-MM-ddTHH:mm:ss.ffffffZ"),
                            updated_at = doctor.LastModificationTime?.ToString("yyyy-MM-ddTHH:mm:ss.ffffffZ") ?? string.Empty,
                            role = roles.Select(r => new UserRole
                            {
                                id = doctor.Id,
                                user_id = user.Id,
                                name = r
                            }).ToList()
                        };
                    }
                }
                else if (role == "Patient")
                {
                    var patient = await _applicationDbContext.Patients_Details
                        .Where(p => p.UserId == user.Id)
                        .FirstOrDefaultAsync();

                    if (patient != null)
                    {
                        userDetails = new UserData
                        {
                            id = patient.Id,
                            f_name = patient.FirstName ?? string.Empty,
                            l_name = patient.LastName ?? string.Empty,
                            phone = user.PhoneNumber ?? string.Empty,
                            isd_code = "+91",
                            gender = patient.Gender ?? string.Empty,
                            dob = patient.DateOfBirth?.ToString("yyyy-MM-dd") ?? string.Empty,
                            email = user.Email ?? string.Empty,
                            address = patient.patient_Address ?? string.Empty,
                            city = patient.city ?? string.Empty,
                            state = patient.State ?? string.Empty,
                            postal_code = int.TryParse(patient.PinCode, out int postalCode) ? postalCode : 0,
                            created_at = patient.CreationTime.ToString("yyyy-MM-ddTHH:mm:ss.ffffffZ"),
                            updated_at = patient.LastModificationTime?.ToString("yyyy-MM-ddTHH:mm:ss.ffffffZ") ?? string.Empty,
                            role = roles.Select(r => new UserRole
                            {
                                id = patient.Id,
                                user_id = user.Id,
                                name = r
                            }).ToList()
                        };
                    }
                }

                if (userDetails == null)
                {
                    return new LoginResponse
                    {
                        response = 404,
                        status = false,
                        message = "User details not found"
                    };
                }

                return new LoginResponse
                {
                    response = 200,
                    status = true,
                    message = "Successfully",
                    data = userDetails,
                    token = token
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<PatientUpdatePassowardResponse> UpdatePatientPassword(PatientUpdatePasswordModel model)
        {
            var response = new PatientUpdatePassowardResponse();

            var user = await _userManager.FindByIdAsync(model.user_id);
            if (user == null)
            {
                response.response = 404;
                response.status = false;
                response.message = "User not found";
                return response;
            }

            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, model.password);

            if (!result.Succeeded)
            {
                response.response = 400;
                response.status = false;
                response.message = "Failed to update password";
                //response.Errors = result.Errors.Select(e => e.Description).ToList();
                return response;
            }

            response.response = 200;
            response.status = true;
            response.message = "Password updated successfully";
            return response;
        }

        public async Task<RegistrationOutputResponse> UpdateUserRoleAsync(string userId, string newRole)
        {
            try
            {
                // Fetch the user by ID
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return new RegistrationOutputResponse
                    {
                        response = 404,
                        status = false,
                        message = "User not found."
                    };
                }

                // Get current roles assigned to the user
                var currentRoles = await _userManager.GetRolesAsync(user);

                // Remove existing roles
                var removeRoleResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!removeRoleResult.Succeeded)
                {
                    return new RegistrationOutputResponse
                    {
                        response = 500,
                        status = false,
                        message = "Failed to remove existing roles. " +
                                  string.Join(", ", removeRoleResult.Errors.Select(e => e.Description))
                    };
                }

                // Add new role
                var addRoleResult = await _userManager.AddToRoleAsync(user, newRole);
                if (!addRoleResult.Succeeded)
                {
                    return new RegistrationOutputResponse
                    {
                        response = 500,
                        status = false,
                        message = "Failed to assign new role. " +
                                  string.Join(", ", addRoleResult.Errors.Select(e => e.Description))
                    };
                }

                return new RegistrationOutputResponse
                {
                    response = 200,
                    status = true,
                    message = $"User role updated successfully to {newRole}."
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

                return new RegistrationOutputResponse
                {
                    response = 500,
                    status = false,
                    message = $"An error occurred: {ex.Message}"
                };
            }
        }

    }
}
