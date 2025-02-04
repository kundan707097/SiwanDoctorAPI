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
        public DoctorAppServices(UserManager<ApplicationUser> userManager, IConfiguration configuration, ApplicationDbContext applicationDbContext, IWebHostEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _configuration = configuration;
            _applicationDbContext = applicationDbContext;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<UpdateDoctorResponse> UpdateDoctorAsync(UpdateDoctorDTO doctorDto)
        {
            var response = new UpdateDoctorResponse();
            var doctor = await _applicationDbContext.Doctor_Details.FindAsync(doctorDto.id);
            if (doctor == null)
            {
                response.response = 404;
                response.status = false;
                response.message = "Doctor not found";
                return response;
            }
            var UserRecords = await _applicationDbContext.Users.Where(u => u.Id == doctor.Id).FirstOrDefaultAsync();
            if (UserRecords == null || doctor == null) {

                return new UpdateDoctorResponse
                {
                    status = false,
                    message = "Doctor details not found."
                };
                
            }
            if (!string.IsNullOrEmpty(doctorDto.email))
            {
                doctor.Email = doctorDto.email;
                UserRecords.Email = doctorDto.email;
            }
            if (!string.IsNullOrEmpty(doctorDto.f_name))
            {
                doctor.FirstName = doctorDto.f_name;
                UserRecords.FirstName = doctorDto.f_name;
            }
            if (!string.IsNullOrEmpty(doctorDto.l_name))
            {
                doctor.LastName = doctorDto.l_name;
                UserRecords.LastName = doctorDto.l_name;
            }
            if (!string.IsNullOrEmpty(doctorDto.isd_code))
            {
                doctor.ISDCode = doctorDto.isd_code;
                UserRecords.ISDCode = doctorDto.isd_code;
            }
            if (!string.IsNullOrEmpty(doctorDto.phone))
            {
                doctor.Phone = doctorDto.phone;
                UserRecords.PhoneNumber = doctorDto.phone;
            }
            if (!string.IsNullOrEmpty(doctorDto.gender))
            {
                doctor.Gender = doctorDto.gender;
                UserRecords.Gender = doctorDto.gender;
            }
            if (!string.IsNullOrEmpty(doctorDto.dob.ToString()))
            {
                doctor.DateOfBirth = doctorDto.dob;
                UserRecords.DateOfBirth = doctorDto.dob;
            }
            //doctor.password = doctorDto.password;
            
            doctor.Department = doctorDto.department.ToString();
            doctor.ExperienceYears = doctorDto.ex_year;
            doctor.IsActive = doctorDto.active;
            doctor.Description= doctorDto.description;
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
            doctor.OpdFee = doctorDto.opd_fee;
            doctor.VideoFee = doctorDto.video_fee;
            doctor.EmergencyFee = doctorDto.emg_fee;


            if (doctorDto.image !=null)
            {
                string webRootPath = _hostingEnvironment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                string patientFolder = Path.Combine(webRootPath, "uploads", doctor.Id.ToString());

                if (!Directory.Exists(patientFolder))
                {
                    Directory.CreateDirectory(patientFolder);
                }

                string uniqueFileName = $"{Guid.NewGuid()}_{doctorDto.image.FileName}";
                string imagePath = Path.Combine(patientFolder, uniqueFileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await doctorDto.image.CopyToAsync(stream);
                }
                doctor.ProfileImagePath = $"/uploads/{doctor.Id}/{uniqueFileName}";
            }
            var result = await _userManager.UpdateAsync(UserRecords);
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
                message = "Failed to Update Records.",
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

        

    }
}
