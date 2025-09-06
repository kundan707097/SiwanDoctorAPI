using Abp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SiwanDoctorAPI.DbConnection;
using SiwanDoctorAPI.Model.EntityModel.Patient_DetailsInformation;
using SiwanDoctorAPI.Model.InputDTOModel.PatientInputDTO;
using SiwanDoctorAPI.Model.InputDTOModel.UpdateInputDTO;
using System.Numerics;
using static SiwanDoctorAPI.DbConnection.ApplicationDbContext;

namespace SiwanDoctorAPI.AppServices.PatientAppServices
{
    public class PatientAppServices : IPatientAppServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public PatientAppServices(UserManager<ApplicationUser> userManager, IConfiguration configuration,
            ApplicationDbContext applicationDbContext, IWebHostEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _configuration = configuration;
            _applicationDbContext = applicationDbContext;
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UpdatePatientResponse> UpdatePatientDetails(UpdatePatientModel updatePatientModel)
        {
            var patientId = int.TryParse(updatePatientModel.Id, out int parsedId) ? parsedId : 0;
            var patientUser = await _applicationDbContext.Patients_Details
                .FirstOrDefaultAsync(x => x.Id == patientId && x.IsDeleted == false);

            if (patientUser == null)
            {
                return new UpdatePatientResponse
                {
                    status = false,
                    message = "User not found."
                };
            }

            var existingUser = await _userManager.FindByIdAsync(patientUser.UserId.ToString());

            if (existingUser == null)
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
                existingUser.Email = updatePatientModel.email;
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

            if (updatePatientModel.image != null)
            {
                string webRootPath = _hostingEnvironment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

                string uploadsFolder = Path.Combine(webRootPath, "uploads", "patient", patientUser.Id.ToString());

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                if (!string.IsNullOrEmpty(patientUser.ProfileImagePath))
                {
                    string webRootPaths = _hostingEnvironment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

                    string userFolder = Path.Combine(webRootPaths, "uploads", "patient", patientUser.Id.ToString());

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


                string fileExtension = Path.GetExtension(updatePatientModel.image.FileName);
                string uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
                string newImagePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(newImagePath, FileMode.Create))
                {
                    await updatePatientModel.image.CopyToAsync(stream);
                }

                string baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";

                patientUser.ProfileImagePath = $"{baseUrl}/uploads/patient/{patientUser.Id}/{uniqueFileName}";
            }

            var result = await _userManager.UpdateAsync(existingUser);
            _applicationDbContext.Patients_Details.Update(patientUser);
            await _applicationDbContext.SaveChangesAsync();

            //if (updatePatientModel.image != null)
            //{
            //    string webRootPath = _hostingEnvironment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            //    string uploadsFolder = Path.Combine(webRootPath, "uploads", patientUser.Id.ToString());

            //    if (!Directory.Exists(uploadsFolder))
            //    {
            //        Directory.CreateDirectory(uploadsFolder);
            //    }

            //    string fileExtension = Path.GetExtension(updatePatientModel.image.FileName);
            //    string uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
            //    string imagePath = Path.Combine(uploadsFolder, uniqueFileName);

            //    using (var stream = new FileStream(imagePath, FileMode.Create))
            //    {
            //        await updatePatientModel.image.CopyToAsync(stream);
            //    }

            //    string baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";

            //    patientUser.ProfileImagePath = $"{baseUrl}/uploads/{patientUser.Id}/{uniqueFileName}";
            //}

            //var result = await _userManager.UpdateAsync(existingUser);
            //_applicationDbContext.Patients_Details.Update(patientUser);
            //await _applicationDbContext.SaveChangesAsync();

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
            var user = await _applicationDbContext.Patients_Details.FirstOrDefaultAsync(x => x.Id == userId && x.IsDeleted ==false);

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
                    image = user.ProfileImagePath,  // ✅ Fix: Get Image Path
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

        //private string GetImagePath(string imagePath)
        //{
        //    string baseUrl = "https://localhost:44306";  // Change this to match your backend URL

        //    if (string.IsNullOrEmpty(imagePath))
        //    {
        //        return $"{baseUrl}/uploads/default.png";  // ✅ Return default image if missing
        //    }

        //    string formattedPath = imagePath.Replace("\\", "/");  // ✅ Fix double slashes
        //    return $"{baseUrl}{formattedPath}";
        //}

        //public async Task<FamilyMemberResponse> AddFamilyMemberAsync(AddFamilyMemberRequest request)
        //{
        //    var response = new FamilyMemberResponse();
        //    try
        //    {
        //        // Check if the user exists
        //        var userExists = await _userManager.Users.AnyAsync(u => u.Id == request.user_id);
        //        if (!userExists)
        //        {
        //            response.response = 404;
        //            response.status = false;
        //            response.message = "User not found";
        //            return response;
        //        }

        //        // Check if family member already exists
        //        var existingMember = await _applicationDbContext.User_FamilyMembers
        //            .FirstOrDefaultAsync(f => f.Phone == request.phone && f.User_Id == request.user_id);

        //        if (existingMember != null)
        //        {
        //            response.response = 409;
        //            response.status = false;
        //            response.message = "Family member already exists with this phone number";
        //            return response;
        //        }
        //        var existingpatient = await _applicationDbContext.Patients_Details.Where(existingMember.User_Id == );




        //        // Create new family member entity
        //        var familyMember = new UserFamilyMember
        //        {
        //            F_Name = request.f_name,
        //            L_Name = request.l_name,
        //            Phone = request.phone,
        //            Isd_Code = request.isd_code,
        //            User_Id = request.user_id,
        //            Gender = request.gender,
        //            Dob = request.dob ?? DateTime.MinValue // Provide a default value
        //        };

        //        // Save to database
        //        _applicationDbContext.User_FamilyMembers.Add(familyMember);
        //        await _applicationDbContext.SaveChangesAsync();

        //        response.response = 200;
        //        response.status = true;
        //        response.message = "Family member added successfully";
        //    }
        //    catch (Exception ex)
        //    {
        //        response.response = 500;
        //        response.status = false;
        //        response.message = $"Error: {ex.Message}";
        //    }

        //    return response;
        //}
        public async Task<FamilyMemberResponse> AddFamilyMemberAsync(AddFamilyMemberRequest request)
        
        {
            var response = new FamilyMemberResponse();
            try
            {
                // Check if the user exists in AspNetUsers
                //var userExists = await _userManager.Users.AnyAsync(u => u.Id == request.user_id);
                //if (!userExists)
                //{
                //    response.response = 404;
                //    response.status = false;
                //    response.message = "User not found";
                //    return response;
                //}

                //// Check if user_id exists in Patient_Information table
                //var existingPatient = await _applicationDbContext.Patients_Details
                //    .FirstOrDefaultAsync(p => p.UserId == request.user_id);

                //if (existingPatient == null)
                //{
                //    response.response = 404;
                //    response.status = false;
                //    response.message = "User not found in Patient_Information table";
                //    return response;
                //}

                // Check if family member already exists
                var existingMember = await _applicationDbContext.User_FamilyMembers
                    .FirstOrDefaultAsync(f => f.Phone == request.phone);

                if (existingMember != null)
                {
                    response.response = 409;
                    response.status = false;
                    response.message = "Family member already exists with this phone number";
                    return response;
                }

                // Create new family member entity
                var familyMember = new UserFamilyMember
                {
                    F_Name = request.f_name,
                    L_Name = request.l_name,
                    Phone = request.phone,
                    Isd_Code = request.isd_code,
                    //User_Id = request.patienttableId,
                    User_Id = request.patienttableId,

                    Gender = request.gender,
                    Dob = request.dob ?? DateTime.MinValue // Provide a default value
                };

                // Save to database
                _applicationDbContext.User_FamilyMembers.Add(familyMember);
                await _applicationDbContext.SaveChangesAsync();

                response.response = 200;
                response.status = true;
                response.message = "Family member added successfully";
            }
            catch (Exception ex)
            {
                response.response = 500;
                response.status = false;
                response.message = $"Error: {ex.Message}";
            }

            return response;
        }


        public async Task<FamilyMemberResponse> AddFamilyMemberbydoctorAsync(AddFamilyMemberRequest request)

        {
            var response = new FamilyMemberResponse();
            try
            {
                // Check if the user exists in AspNetUsers
                //var userExists = await _userManager.Users.AnyAsync(u => u.Id == request.user_id);
                //if (!userExists)
                //{
                //    response.response = 404;
                //    response.status = false;
                //    response.message = "User not found";
                //    return response;
                //}

                //// Check if user_id exists in Patient_Information table
                //var existingPatient = await _applicationDbContext.Patients_Details
                //    .FirstOrDefaultAsync(p => p.UserId == request.user_id);

                //if (existingPatient == null)
                //{
                //    response.response = 404;
                //    response.status = false;
                //    response.message = "User not found in Patient_Information table";
                //    return response;
                //}

                // Check if family member already exists
                var existingMember = await _applicationDbContext.User_FamilyMembers
                    .FirstOrDefaultAsync(f => f.Phone == request.phone);

                if (existingMember != null)
                {
                    response.response = 409;
                    response.status = false;
                    response.message = "Family member already exists with this phone number";
                    return response;
                }

                // Create new family member entity
                var familyMember = new UserFamilyMember
                {
                    F_Name = request.f_name,
                    L_Name = request.l_name,
                    Phone = request.phone,
                    Isd_Code = request.isd_code,
                    //User_Id = request.patienttableId,
                    User_Id = request.user_id,

                    Gender = request.gender,
                    Dob = request.dob ?? DateTime.MinValue // Provide a default value
                };

                // Save to database
                _applicationDbContext.User_FamilyMembers.Add(familyMember);
                await _applicationDbContext.SaveChangesAsync();

                response.response = 200;
                response.status = true;
                response.message = "Family member added successfully";
            }
            catch (Exception ex)
            {
                response.response = 500;
                response.status = false;
                response.message = $"Error: {ex.Message}";
            }

            return response;
        }



        public async Task<List<GetFamilyMemberResponse>> GetFamilyMembersByUserAsync(int userId)
        {
            var responseList = new List<GetFamilyMemberResponse>();

            try
            {
                var familyMembers = await _applicationDbContext.User_FamilyMembers
                    .Where(x => x.User_Id == userId && x.IsDeleted == false)
                    .ToListAsync();

                

                responseList = familyMembers.Select(fm => new GetFamilyMemberResponse
                {
                    id= fm.Id,
                    f_name = fm.F_Name,
                    l_name = fm.L_Name,
                    phone = fm.Phone,
                    isd_code = fm.Isd_Code,
                    user_id = fm.User_Id,
                    gender = fm.Gender,
                    dob = fm.Dob,
                    created_at = fm.CreationTime.ToString(),
                    updated_at = fm.LastModificationTime.ToString(),
                }).ToList();
            }
            catch (Exception ex)
            {
                
            }

            return responseList;
        }

        public async Task<bool> DeleteFamilyMemberByIdAsync(int id)
        {
            var familyMember = await _applicationDbContext.User_FamilyMembers
                .FirstOrDefaultAsync(fm => fm.Id == id);

            if (familyMember == null)
                return false;

            // Set IsDelete to true instead of removing the record
            familyMember.IsDeleted = true;
            _applicationDbContext.User_FamilyMembers.Update(familyMember);

            await _applicationDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateFamilyMemberAsync(UpdateFamilyMemberDto input)
        {
            var familyMember = await _applicationDbContext.User_FamilyMembers
                .FirstOrDefaultAsync(fm => fm.Id == input.id && fm.IsDeleted ==false);

            if (familyMember == null)
                return false;
            // Updating fields
            familyMember.F_Name = input.f_name;
            familyMember.L_Name = input.l_name;
            familyMember.Phone = input.phone;
            familyMember.Isd_Code = input.isd_code;
            familyMember.Gender = input.gender;
            if (DateTime.TryParse(input.dob, out DateTime parsedDob))
            {
                familyMember.Dob = parsedDob;
            }
            else
            {
                return false; // Handle invalid date format
            }

            _applicationDbContext.User_FamilyMembers.Update(familyMember);
            await _applicationDbContext.SaveChangesAsync();

            return true;
        }

    }
}
