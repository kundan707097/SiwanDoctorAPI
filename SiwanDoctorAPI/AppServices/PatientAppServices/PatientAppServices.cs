using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SiwanDoctorAPI.DbConnection;
using SiwanDoctorAPI.Model.InputDTOModel.PatientInputDTO;
using SiwanDoctorAPI.Model.InputDTOModel.UpdateInputDTO;
using static SiwanDoctorAPI.DbConnection.ApplicationDbContext;

namespace SiwanDoctorAPI.AppServices.PatientAppServices
{
    public class PatientAppServices : IPatientAppServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public PatientAppServices(UserManager<ApplicationUser> userManager, IConfiguration configuration,
            ApplicationDbContext applicationDbContext, IWebHostEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _configuration = configuration;
            _applicationDbContext = applicationDbContext;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<UpdatePatientResponse> UpdatePatientDetails(UpdatePatientModel updatePatientModel)
        {
            var existingUser = await _userManager.FindByIdAsync(updatePatientModel.Id);
            var patientId = int.TryParse(updatePatientModel.Id, out int parsedId) ? parsedId : 0;
            var patientUser = await _applicationDbContext.Patients_Details.FirstOrDefaultAsync(x => x.UserId == patientId);


            if (patientUser == null || existingUser == null)
            {
                return new UpdatePatientResponse
                {
                    status = false,
                    message = "User not found."
                };
            }

            if (!string.IsNullOrEmpty(updatePatientModel.email))
            {
                existingUser.Email = updatePatientModel.email;
                existingUser.UserName = updatePatientModel.email;
                patientUser.Email = updatePatientModel.email;
            }

            if (!string.IsNullOrEmpty(updatePatientModel.f_name))
            {
                existingUser.FirstName = updatePatientModel.f_name;
                patientUser.FirstName = updatePatientModel.f_name;
            }

            if (!string.IsNullOrEmpty(updatePatientModel.l_last))
            {
                existingUser.LastName = updatePatientModel.l_last;
                patientUser.LastName = updatePatientModel.l_last;
            }

            if (!string.IsNullOrEmpty(updatePatientModel.isd_code))
            {
                existingUser.ISDCode = updatePatientModel.isd_code;
                patientUser.ISDCode = updatePatientModel.isd_code;
            }

            if (!string.IsNullOrEmpty(updatePatientModel.phone))
            {
                existingUser.PhoneNumber = updatePatientModel.phone;
                patientUser.Phone = updatePatientModel.phone;
            }

            if (!string.IsNullOrEmpty(updatePatientModel.gender))
            {
                existingUser.Gender = updatePatientModel.gender;
                patientUser.Gender = updatePatientModel.gender;
            }

            // Handle image upload if provided
            if (updatePatientModel.image != null)
            {
                string webRootPath = _hostingEnvironment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                string patientFolder = Path.Combine(webRootPath, "uploads", existingUser.Id.ToString());

                if (!Directory.Exists(patientFolder))
                {
                    Directory.CreateDirectory(patientFolder);
                }

                string uniqueFileName = $"{Guid.NewGuid()}_{updatePatientModel.image.FileName}";
                string imagePath = Path.Combine(patientFolder, uniqueFileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await updatePatientModel.image.CopyToAsync(stream);
                }
                patientUser.ProfileImagePath = $"/uploads/{existingUser.Id}/{uniqueFileName}";
            }

            var result = await _userManager.UpdateAsync(existingUser);
            _applicationDbContext.Patients_Details.Update(patientUser);
            await _applicationDbContext.SaveChangesAsync();

            if (result.Succeeded)
            {
                return new UpdatePatientResponse
                {
                    status = true,
                    message = "User updated successfully.",
                    response = 200
                };
            }

            return new UpdatePatientResponse
            {
                status = false,
                message = "Failed to update user.",
                response = 400
            };
        }

        public async Task<PatientResponseModel> GetUserByIdAsync(int userId)
        {
            var user = await _applicationDbContext.Patients_Details.FirstOrDefaultAsync(x => x.UserId == userId);

            if (user == null)
            {
                return new PatientResponseModel
                {
                    response = 404,
                    data = null
                };
            }

            return new PatientResponseModel
            {
                response = 200,
                data = new Data
                {
                    id = user.Id,
                    UserId = user.UserId,
                    f_name = user.FirstName,
                    l_name = user.LastName,
                    phone = user.Phone,
                    isd_code = user.ISDCode,
                    gender = user.Gender,
                    dob = user.DateOfBirth ?? DateTime.MinValue,  // ✅ Fix: Handle null value
                    email = user.Email,
                    image = GetImagePath(user.ProfileImagePath),  // ✅ Fix: Get Image Path
                    address = user.patient_Address,
                    city = user.city,
                    state = user.State,
                    postal_code = user.PinCode,
                    created_at = user.CreationTime,
                    updated_at = user.LastModificationTime ?? DateTime.MinValue,  // ✅ Fix: Handle null value
                    is_deleted = user.IsDeleted
                }
            };

        }

        private string GetImagePath(string imagePath)
        {
            string baseUrl = "https://localhost:44306";  // Change this to match your backend URL

            if (string.IsNullOrEmpty(imagePath))
            {
                return $"{baseUrl}/uploads/default.png";  // ✅ Return default image if missing
            }

            string formattedPath = imagePath.Replace("\\", "/");  // ✅ Fix double slashes
            return $"{baseUrl}{formattedPath}";
        }



    }
}
