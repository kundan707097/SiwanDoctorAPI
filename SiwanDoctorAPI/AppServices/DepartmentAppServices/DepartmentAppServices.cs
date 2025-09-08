using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SiwanDoctorAPI.DbConnection;
using SiwanDoctorAPI.Model.EntityModel.DoctorEntity;
using SiwanDoctorAPI.Model.InputDTOModel.DepartmentInputDTO;
using static SiwanDoctorAPI.DbConnection.ApplicationDbContext;

namespace SiwanDoctorAPI.AppServices.DepartmentAppServices
{
    public class DepartmentAppServices : IDepartmentAppServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DepartmentAppServices(UserManager<ApplicationUser> userManager, IConfiguration configuration, ApplicationDbContext applicationDbContext, IWebHostEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            
            _userManager = userManager;
            _configuration = configuration;
            _applicationDbContext = applicationDbContext;
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AddDepartmentResponse> AddDepartmentAsync(AddDepartmentRequest request)
        {
            try
            {
                string fileName = null;
                string imageUrl = null;

                if (request.image != null)
                {
                    // Get web root path (ensure it's not null)
                    string webRootPath = _hostingEnvironment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

                    // Define the folder path where images will be stored
                    string uploadsFolder = Path.Combine(webRootPath, "uploads", "department_images");

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.image.FileName)}";

                    string filePath = Path.Combine(uploadsFolder, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.image.CopyToAsync(fileStream);
                    }

                    string baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
                    imageUrl = $"{baseUrl}/uploads/department_images/{fileName}";
                }

                var department = new Department
                {
                    Title = request.title,
                    Description = request.description,
                    DepartmentImage = imageUrl,  // Store the full image URL
                    CreationTime = DateTime.UtcNow, // Use UTC for consistency
                    LastModificationTime = DateTime.UtcNow
                };

                _applicationDbContext.Doctor_Departments.Add(department);
                await _applicationDbContext.SaveChangesAsync();

                return new AddDepartmentResponse
                {
                    response = 200,
                    status = true,
                    message = "Successfully added department",
                    id = department.Id,
                };
            }
            catch (Exception ex)
            {
                return new AddDepartmentResponse
                {
                    response = 400,
                    status = false,
                    message = $"Error: {ex.Message}"
                };
            }
        }


        public async Task<UpdateDepartmentResponse> UpdateDepartmentAsync(UpdateDepartmentRequest request)
        {
            try
            {
                // Find the department by ID
                var department = await _applicationDbContext.Doctor_Departments.FirstOrDefaultAsync(d => d.Id == request.id);

                if (department == null)
                {
                    return new UpdateDepartmentResponse
                    {
                        response = 404,
                        status = false,
                        message = "Department not found"
                    };
                }

                // Update the 'title' and 'description' fields if provided
                if (!string.IsNullOrEmpty(request.title))
                {
                    department.Title = request.title;
                }

                if (!string.IsNullOrEmpty(request.description))
                {
                    department.Description = request.description;
                }

                // Handle the image update if a new image is provided
                if (request.image != null)
                {
                    // Get web root path (ensure it's not null)
                    string webRootPath = _hostingEnvironment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

                    // Define the folder path where images will be stored
                    string uploadsFolder = Path.Combine(webRootPath, "uploads", "department_images");

                    // Ensure the folder exists
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Generate a unique file name for the uploaded image
                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.image.FileName)}";
                    string filePath = Path.Combine(uploadsFolder, fileName);

                    // Save the new image to the server
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.image.CopyToAsync(fileStream);
                    }

                    // Delete the old image if it exists
                    if (!string.IsNullOrEmpty(department.DepartmentImage))
                    {
                        string oldImagePath = Path.Combine(webRootPath, department.DepartmentImage.TrimStart('/'));
                        if (File.Exists(oldImagePath))
                        {
                            File.Delete(oldImagePath);
                        }
                    }

                    string baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
                    string imageUrl = $"{baseUrl}/uploads/department_images/{fileName}";

                    department.DepartmentImage = imageUrl;
                }

                department.LastModificationTime = DateTime.UtcNow;

                await _applicationDbContext.SaveChangesAsync();

                return new UpdateDepartmentResponse
                {
                    response = 200,
                    status = true,
                    message = "Successfully updated department",
                };
            }
            catch (Exception ex)
            {

                return new UpdateDepartmentResponse
                {
                    response = 400,
                    status = false,
                    message = $"Error: {ex.Message}"
                };
            }
        }



        public async Task<RemoveDepartmentImageResponse> RemoveDepartmentImageAsync(int id)
        {
            try
            {
                // Find the department by ID
                var department = await _applicationDbContext.Doctor_Departments.FirstOrDefaultAsync(d => d.Id == id && d.IsDeleted==false);

                if (department == null)
                {
                    return new RemoveDepartmentImageResponse
                    {
                        response = 404,
                        status = false,
                        message = "Department not found"
                    };
                }

                // Check if the department has an image
                if (string.IsNullOrEmpty(department.DepartmentImage))
                {
                    return new RemoveDepartmentImageResponse
                    {
                        response = 400,
                        status = false,
                        message = "No image to remove"
                    };
                }

                // Define the image file path
                var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, department.DepartmentImage.TrimStart('/'));

                // Check if the file exists, and delete it if it does
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }

                // Remove the image path from the database
                department.DepartmentImage = null;

                // Update the department record
                await _applicationDbContext.SaveChangesAsync();

                return new RemoveDepartmentImageResponse
                {
                    response = 200,
                    status = true,
                    message = "Successfully removed department image"
                };
            }
            catch (Exception ex)
            {
                return new RemoveDepartmentImageResponse
                {
                    response = 500,
                    status = false,
                    message = $"Error: {ex.Message}"
                };
            }
        }
        public async Task<GetDepartmentResponse> GetDepartmentsAsync()
        {
            var departments = await _applicationDbContext.Doctor_Departments.Where(x=>x.IsDeleted==false)
                .Select(d => new DepartmentDto
                {
                    id = d.Id,
                    title = d.Title,
                    description = d.Description,
                    image = d.DepartmentImage,
                    active = d.IsDeleted ? 1 : 0,
                    createdAt = d.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    updatedAt = d.LastModificationTime.ToString()
                })
                .ToListAsync();

            return new GetDepartmentResponse
            {
                response = 200,
                data = departments
            };
        }
        public async Task<GetDepartmentResponse> GetActiveDepartmentsAsync()
        {
            var departments = await _applicationDbContext.Doctor_Departments.Where(x => x.IsDeleted == false)
               .Select(d => new DepartmentDto
               {
                   id = d.Id,
                   title = d.Title,
                   description = d.Description,
                   image = d.DepartmentImage,
                   active = d.Active ? 1 : 0,
                
                   createdAt = d.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                   updatedAt = d.LastModificationTime.ToString()
               }).Where(i => i.active == 1).ToListAsync();
               
            return new GetDepartmentResponse
            {
                response = 200,
                data = departments
            };
        }

            public async Task<GetSingleDepartmentResponse> GetDepartmentByIdAsync(int id)
        {
            var department = await _applicationDbContext.Doctor_Departments
                .Where(d => d.Id == id && d.IsDeleted ==false)
                .Select(d => new DepartmentDto
                {
                    id = d.Id,
                    title = d.Title,
                    description = d.Description,
                    image = d.DepartmentImage,
                    active = d.IsDeleted ? 1 : 0,
                    createdAt = d.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    updatedAt = d.LastModificationTime.ToString()
                })
                .FirstOrDefaultAsync();

            if (department == null)
            {
                return new GetSingleDepartmentResponse
                {
                    Response = 404,
                    Data = null
                };
            }

            return new GetSingleDepartmentResponse
            {
                Response = 200,
                Data = department
            };
        }

        public async Task<DeleteDepartmentResponse> DeleteDepartmentAsync(int id)
        {
            var department = await _applicationDbContext.Doctor_Departments.FindAsync(id);

            if (department == null)
            {
                return new DeleteDepartmentResponse
                {
                    response = 401,
                    status = false,
                    message = "Department not found."
                };
            }

            _applicationDbContext.Doctor_Departments.Remove(department);
            await _applicationDbContext.SaveChangesAsync();

            return new DeleteDepartmentResponse
            {
                response = 200,
                status = true,
                message = "Department deleted successfully."
            };
        }
    }
}
