using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SiwanDoctorAPI.DbConnection;
using SiwanDoctorAPI.Model.EntityModel.Department;
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
        public DepartmentAppServices(UserManager<ApplicationUser> userManager, IConfiguration configuration, ApplicationDbContext applicationDbContext, IWebHostEnvironment hostingEnvironment)
        {
            
            _userManager = userManager;
            _configuration = configuration;
            _applicationDbContext = applicationDbContext;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<AddDepartmentResponse> AddDepartmentAsync(AddDepartmentRequest request)
        {
            try
            {
                string fileName = null;
                if (request.image != null)
                {
                    // Define the folder path where images will be stored
                    var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", "department_images");

                    // Check if the folder exists, if not, create it
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);  // Ensure the folder exists
                    }

                    // Generate a unique file name for the uploaded image
                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.image.FileName);

                    // Define the full file path where the image will be saved
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    // Save the image to the server
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.image.CopyToAsync(fileStream);
                    }
                }

                // Create the department object to save in the database
                var department = new Department
                {
                    Title = request.title,
                    Description = request.description,
                    DepartmentImage = fileName != null ? Path.Combine("uploads", "department_images", fileName) : null,
                    CreationTime = DateTime.Now,
                    LastModificationTime = DateTime.Now
                };

                // Add the department to the database
                _applicationDbContext.Doctor_Departments.Add(department);
                await _applicationDbContext.SaveChangesAsync();

                // Return the response with success status
                return new AddDepartmentResponse
                {
                    response = 200,
                    status = true,
                    message = "Successfully added department",
                    id = department.Id
                };
            }
            catch (Exception ex)
            {
                // Return the response with error status if an exception occurs
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

                // Update the 'active' status
                //department.IsActive = request.Active == 1; // Set true if Active is 1, else false

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
                    // Define the folder path where images will be stored
                    var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", "department_images");

                    // Check if the folder exists, if not, create it
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);  // Ensure the folder exists
                    }

                    // Generate a unique file name for the uploaded image
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.image.FileName);
                    string filePath = Path.Combine(uploadsFolder, fileName);

                    // Save the new image to the server
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.image.CopyToAsync(fileStream);
                    }

                    // Update the department's image path in the database
                    department.DepartmentImage = Path.Combine("uploads", "department_images", fileName);
                }

                // Update the modified timestamp
                department.LastModificationTime = DateTime.Now;

                // Save changes to the database
                await _applicationDbContext.SaveChangesAsync();

                // Return success response
                return new UpdateDepartmentResponse
                {
                    response = 200,
                    status = true,
                    message = "Successfully updated department"
                };
            }
            catch (Exception ex)
            {
                // Log exception (not shown here)
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
                    response = 201,
                    status = false,
                    message = "Department not found."
                };
            }

            _applicationDbContext.Doctor_Departments.Remove(department);
            await _applicationDbContext.SaveChangesAsync();

            return new DeleteDepartmentResponse
            {
                response = 201,
                status = true,
                message = "Department deleted successfully."
            };
        }
    }
}
