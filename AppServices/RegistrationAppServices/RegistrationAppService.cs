using Castle.MicroKernel.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SiwanDoctorAPI.DbConnection;
using SiwanDoctorAPI.Model.EntityModel.Doctor_DetailsInformation;
using SiwanDoctorAPI.Model.EntityModel.Patient_DetailsInformation;
using SiwanDoctorAPI.Model.InputDTOModel.RegistrationInputDTO;
using System.Numerics;
using static SiwanDoctorAPI.DbConnection.ApplicationDbContext;

namespace SiwanDoctorAPI.AppServices.RegistrationAppServices
{
    public class RegistrationAppService: IRegistrationAppService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _applicationDbContext;
        public RegistrationAppService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole<int>> roleManager, IConfiguration configuration, ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _applicationDbContext = applicationDbContext;
        }


        public async Task<RegistrationOutputResponse> PatientsAndDoctorRegistration(RegisterModel registerModel)
        {
            try
            {
              
                var existingUser = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == registerModel.phone);
                if (existingUser != null)
                {
                    return new RegistrationOutputResponse
                    {
                        message = "Mobile number is already registered. Please log in.",
                        status = false
                    };
                }

                // Generate registration number based on user type
                string registrationNo = registerModel.UserType switch
                {
                    1 => GenerateUniqueBookingNumber("D"), // Doctor
                    2 => GenerateUniqueBookingNumber("P"), // Patient
                    _ => throw new ArgumentException("Invalid UserType")
                };

                // Create a new user object
                var newUser = new ApplicationUser
                {
                    UserName = registerModel.phone,
                    PhoneNumber = registerModel.phone,
                    UserType = (ApplicationDbContext.UserType)registerModel.UserType,
                    FirstName = registerModel.f_name,
                    LastName = registerModel.l_name,
                    ISDCode = registerModel.isd_code,
                    Gender = registerModel.gender,
                    RegistrationNo = registrationNo
                };

                // Create the user in UserManager
                var createResult = await _userManager.CreateAsync(newUser, registerModel.password);
                if (!createResult.Succeeded)
                {
                    return new RegistrationOutputResponse
                    {
                        message = "Failed to create user. Errors: " + string.Join(", ", createResult.Errors.Select(e => e.Description)),
                        status = false,
                        response = 500
                    };
                }

                // Fetch the newly created user
                var createdUser = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == registerModel.phone);
                if (createdUser == null)
                {
                    return new RegistrationOutputResponse
                    {
                        message = "User creation failed.",
                        status = false,
                        response = 500
                    };
                }

                // Add user to a role and insert data into the respective table
                if (registerModel.UserType == 1) // Doctor
                {
                    var roleResult = await AssignRoleToUser(createdUser, "Doctor");
                    if (!roleResult.status)
                    {
                        return roleResult;
                    }

                    var doctorDetails = new Doctor_Details
                    {
                        UserId = createdUser.Id,
                        FirstName = createdUser.FirstName,
                        LastName = createdUser.LastName,
                        Phone = registerModel.phone,
                        ISDCode = registerModel.isd_code,
                        Gender = registerModel.gender,
                        registrationNo = registrationNo
                    };

                    _applicationDbContext.Doctor_Details.Add(doctorDetails);
                }
                else if (registerModel.UserType == 2) // Patient
                {
                    var roleResult = await AssignRoleToUser(createdUser, "Patient");
                    if (!roleResult.status)
                    {
                        return roleResult;
                    }

                    var patientDetails = new Patient_Details
                    {
                        UserId = createdUser.Id,
                        FirstName = createdUser.FirstName,
                        LastName = createdUser.LastName,
                        Phone = registerModel.phone,
                        ISDCode = registerModel.isd_code,
                        Gender = registerModel.gender,
                        registrationNo = registrationNo
                    };

                    _applicationDbContext.Patients_Details.Add(patientDetails);
                }

                await _applicationDbContext.SaveChangesAsync();

                return new RegistrationOutputResponse
                {
                    response = 200,
                    status = true,
                    message = "Successfully registered",
                    Id = createdUser.Id.ToString(),

                };
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine($"Error: {ex.Message}");

                return new RegistrationOutputResponse
                {
                    response = 500,
                    status = false,
                    message = $"An error occurred: {ex.Message}"
                };
            }
        }




        private string GenerateUniqueBookingNumber(string Prefix)
        {
            // Get the current date and time
            DateTime now = DateTime.Now;

            string formattedDateTime = now.ToString("MMddHHmmssfff"); 

            string uniqueNumber = $"{Prefix}{formattedDateTime.Substring(2)}";

            return uniqueNumber;
        }

        private async Task<RegistrationOutputResponse> AssignRoleToUser(ApplicationUser user, string roleName)
        {
            
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                // Create a new role using IdentityRole<int>
                var roleResult = await _roleManager.CreateAsync(new IdentityRole<int> { Name = roleName });
                if (!roleResult.Succeeded)
                {
                    return new RegistrationOutputResponse
                    {
                        response = 500,
                        status = false,
                        message = "Failed to create role."
                    };
                }
            }

            // Assign the role to the user
            var addToRoleResult = await _userManager.AddToRoleAsync(user, roleName);
            if (!addToRoleResult.Succeeded)
            {
                return new RegistrationOutputResponse
                {
                    response = 500,
                    status = false,
                    message = "Failed to assign role to user."
                };
            }

            return new RegistrationOutputResponse
            {
                response = 200,
                status = true,
                message = "Role assigned successfully"
            };
        }

    }
}
