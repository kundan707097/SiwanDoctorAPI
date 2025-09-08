using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SiwanDoctorAPI.DbConnection;
using SiwanDoctorAPI.Model.InputDTOModel.DoctorInputDTO;
using SiwanDoctorAPI.Model.InputDTOModel.UpdateInputDTO;
using static SiwanDoctorAPI.DbConnection.ApplicationDbContext;

namespace SiwanDoctorAPI.AppServices.DoctorAppServices
{
    public class DoctorAppServices : IDoctorAppServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DoctorAppServices(UserManager<ApplicationUser> userManager, IConfiguration configuration, ApplicationDbContext applicationDbContext, IWebHostEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _configuration = configuration;
            _applicationDbContext = applicationDbContext;
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UpdateDoctorResponse> UpdateDoctorAsync(UpdateDoctorDTO doctorDto)
        {
            var response = new UpdateDoctorResponse();

            // Fetch doctor details
            var doctor = await _applicationDbContext.Doctor_Details.FindAsync(doctorDto.id);
            if (doctor == null)
            {
                return new UpdateDoctorResponse
                {
                    response = 404,
                    status = false,
                    message = "Doctor not found"
                };
            }

            var userRecord = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Id == doctor.UserId);
            if (userRecord == null)
            {
                return new UpdateDoctorResponse
                {
                    response = 201,
                    status = false,
                    message = "Doctor details not found."
                };
            }

            if (!string.IsNullOrEmpty(doctorDto.email))
            {
                doctor.Email = doctorDto.email;
                userRecord.Email = doctorDto.email;
            }
            if (!string.IsNullOrEmpty(doctorDto.f_name))
            {
                doctor.FirstName = doctorDto.f_name;
                userRecord.FirstName = doctorDto.f_name;
            }
            if (!string.IsNullOrEmpty(doctorDto.l_name))
            {
                doctor.LastName = doctorDto.l_name;
                userRecord.LastName = doctorDto.l_name;
            }
            if (!string.IsNullOrEmpty(doctorDto.isd_code))
            {
                doctor.ISDCode = doctorDto.isd_code;
                userRecord.ISDCode = doctorDto.isd_code;
            }
            if (!string.IsNullOrEmpty(doctorDto.phone))
            {
                doctor.Phone = doctorDto.phone;
                userRecord.PhoneNumber = doctorDto.phone;
                userRecord.UserName = doctorDto.phone;
            }
            if (!string.IsNullOrEmpty(doctorDto.gender))
            {
                doctor.Gender = doctorDto.gender;
                userRecord.Gender = doctorDto.gender;
            }
            if (doctorDto.dob != null)
            {
                doctor.DateOfBirth = doctorDto.dob;
                userRecord.DateOfBirth = doctorDto.dob;
            }

            doctor.DepartmentId = doctorDto.department;
            doctor.ExperienceYears = doctorDto.ex_year;
            doctor.IsActive = doctorDto.active;
            doctor.Description = doctorDto.description;
            doctor.Specialization = doctorDto.specialization;
            doctor.doctor_Address = doctorDto.address;
            doctor.State = doctorDto.state;
            doctor.city = doctorDto.city;
            doctor.PostalCode = doctorDto.postal_code;
            doctor.ZoomClientId = doctorDto.zoom_client_id;
            doctor.ZoomSecretId = doctorDto.zoom_secret_id;
            doctor.FacebookLink = doctorDto.fb_linik;
            doctor.TwitterLink = doctorDto.twitter_link;
            doctor.YouTubeLink = doctorDto.you_tube_link;
            doctor.InstagramLink = doctorDto.insta_link;
            doctor.OpdFee = doctorDto.opd_fee ?? 0;
            doctor.VideoFee = doctorDto.video_fee ?? 0;
            doctor.EmergencyFee = doctorDto.emg_fee ?? 0;

            if (doctorDto.image != null)
            {
                string webRootPath = _hostingEnvironment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                string doctorFolder = Path.Combine(webRootPath, "uploads", "doctor", doctor.Id.ToString());

                if (!Directory.Exists(doctorFolder))
                {
                    Directory.CreateDirectory(doctorFolder);
                }

                if (!string.IsNullOrEmpty(doctor.ProfileImagePath))
                {
                    string webRootPaths = _hostingEnvironment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

                    string userFolder = Path.Combine(webRootPaths, "uploads", "doctor", doctor.Id.ToString());

                    if (Directory.Exists(userFolder))
                    {
                        string[] files = Directory.GetFiles(userFolder);

                        foreach (string file in files)
                        {
                            try
                            {
                                File.Delete(file);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error deleting {file}: {ex.Message}");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Folder not found: {userFolder}");
                    }

                }

                string fileExtension = Path.GetExtension(doctorDto.image.FileName);
                string uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
                string newImagePath = Path.Combine(doctorFolder, uniqueFileName);

                using (var stream = new FileStream(newImagePath, FileMode.Create))
                {
                    await doctorDto.image.CopyToAsync(stream);
                }

                string baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
                doctor.ProfileImagePath = $"{baseUrl}/uploads/doctor/{doctor.Id}/{uniqueFileName}";
            }

            var result = await _userManager.UpdateAsync(userRecord);
            _applicationDbContext.Doctor_Details.Update(doctor);
            await _applicationDbContext.SaveChangesAsync();

            if (result.Succeeded)
            {
                return new UpdateDoctorResponse
                {
                    status = true,
                    message = "Records updated successfully.",
                    response = 200
                };
            }

            return new UpdateDoctorResponse
            {
                status = false,
                message = "Failed to update records.",
                response = 400
            };
        }


        public async Task<UpdateDoctorResponse> RemoveDoctorImageAsync(int id)
        {
            var response = new UpdateDoctorResponse();
            var doctor = await _applicationDbContext.Doctor_Details.FindAsync(id);
            if (doctor == null)
            {
                response.response = 404;
                response.status = false;
                response.message = "Doctor not found";
                return response;
            }

            if (string.IsNullOrEmpty(doctor.ProfileImagePath))
            {
                response.response = 400;
                response.status = false;
                response.message = "No image found for this doctor.";
                return response;
            }

            string webRootPath = _hostingEnvironment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string imagePath = Path.Combine(webRootPath, doctor.ProfileImagePath.TrimStart('/'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            doctor.ProfileImagePath = null;
            _applicationDbContext.Doctor_Details.Update(doctor);
            await _applicationDbContext.SaveChangesAsync();
            response.response = 200;
            response.status = true;
            response.message = "Image removed successfully.";
            return response;
        }

        public async Task<UpdateDoctorResponse> UpdateDoctorImageAsync(DoctorUpdateImage request)
        {
            var response = new UpdateDoctorResponse();
            var doctor = await _applicationDbContext.Doctor_Details.FindAsync(request.id);

            if (doctor == null)
            {
                return new UpdateDoctorResponse
                {
                    response = 404, // Not Found
                    status = false,
                    message = "Doctor not found."
                };
            }

            if (string.IsNullOrEmpty(doctor.ProfileImagePath))
            {
                //return new UpdateDoctorResponse
                //{
                //    response = 400, // Bad Request
                //    status = false,
                //    message = "No image found for this doctor."
                //};
            }

            if (request.image != null)
            {
                string webRootPath = _hostingEnvironment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                string doctorFolder = Path.Combine(webRootPath, "uploads", "doctor", doctor.Id.ToString());

                if (!Directory.Exists(doctorFolder))
                {
                    Directory.CreateDirectory(doctorFolder);
                }

                // Delete existing image
                if (!string.IsNullOrEmpty(doctor.ProfileImagePath))
                {
                    string userFolder = Path.Combine(webRootPath, "uploads", "doctor", doctor.Id.ToString());

                    if (Directory.Exists(userFolder))
                    {
                        string[] files = Directory.GetFiles(userFolder);
                        foreach (string file in files)
                        {
                            try
                            {
                                File.Delete(file);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error deleting {file}: {ex.Message}");
                            }
                        }
                    }
                }

                // Save new image
                string fileExtension = Path.GetExtension(request.image.FileName);
                string uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
                string newImagePath = Path.Combine(doctorFolder, uniqueFileName);

                using (var stream = new FileStream(newImagePath, FileMode.Create))
                {
                    await request.image.CopyToAsync(stream);
                }

                string baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
                doctor.ProfileImagePath = $"{baseUrl}/uploads/doctor/{doctor.Id}/{uniqueFileName}";
            }

            _applicationDbContext.Doctor_Details.Update(doctor);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
                return new UpdateDoctorResponse
                {
                    response = 200, // OK
                    status = true,
                    message = "Record updated successfully."
                };
            }
            catch (Exception)
            {
                return new UpdateDoctorResponse
                {
                    response = 500, // Internal Server Error
                    status = false,
                    message = "An error occurred while updating the record."
                };
            }
        }

    }
}
