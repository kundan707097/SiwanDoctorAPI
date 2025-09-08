using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SiwanDoctorAPI.DbConnection;
using SiwanDoctorAPI.Model.EntityModel.AdminEntity;
using SiwanDoctorAPI.Model.InputDTOModel.SocialMediaInputDTO;
using static SiwanDoctorAPI.DbConnection.ApplicationDbContext;

namespace SiwanDoctorAPI.AppServices.SocialMediaAppservices
{
    public class SocialMediaAppservices: ISocialMediaAppservices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public SocialMediaAppservices(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, IConfiguration configuration, ApplicationDbContext applicationDbContext, IWebHostEnvironment hostingEnvironment)
        {
            
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _configuration = configuration;
            _applicationDbContext = applicationDbContext;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<SocialMediaResponse> AddSocialMediaAsync(SocialMediaRequest request)
        {
            try
            {
                string fileName = null;
                string imageUrl = null;
                if (request.image != null)
                {
                    string webRootPath = _hostingEnvironment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

                    string uploadsFolder = Path.Combine(webRootPath, "uploads", "socialmedia_images");
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
                    imageUrl = $"{baseUrl}/uploads/socialmedia_images/{fileName}";
                }
                var socialMedia = new SocialMedia
                {
                    Title = request.title,
                    SocialMediaProfile = imageUrl,
                    Url = request.url,
                };
                await _applicationDbContext.socialMedias.AddAsync(socialMedia);
                await _applicationDbContext.SaveChangesAsync();
                return new SocialMediaResponse
                {
                    response = 200,
                    status = true,
                    message = "successfully"
                };
            }
            catch (Exception ex)
            {
                return new SocialMediaResponse
                {
                    response = 500,
                    status = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        public async Task<UpdateSocialMediaResponse> UpdateSocialMediaAsync(UpdateSocialMediaRequest request)
        {
            var response =  await _applicationDbContext.socialMedias.Where(x=>x.Id ==request.id).FirstOrDefaultAsync();
            if (response == null)
            {
                return new UpdateSocialMediaResponse
                {
                    response = 500,
                    status = false,
                    message = "Records Not Found"
                };
            }
            string fileName = null;
            string imageUrl = null;
            
            if (request.image != null)
            {
                string webRootPath = _hostingEnvironment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

                string uploadsFolder = Path.Combine(webRootPath, "uploads", "socialmedia_images");
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
                imageUrl = $"{baseUrl}/uploads/socialmedia_images/{fileName}";
            }
            else
            {
                imageUrl =null ;
            }

            response.Title = request.title;
            response.Url = request.url;
            response.SocialMediaProfile = imageUrl ;

             _applicationDbContext.socialMedias.Update(response);
            await _applicationDbContext.SaveChangesAsync();

            return new UpdateSocialMediaResponse
            {
                response = 200,
                status = true,
                message = "Records Update Sucessfully"
            };

        }

        public async Task<UpdateSocialMediaResponse> RemoveSocialMediaImageAsync(RemoveSocialMediaImage request)
        {
            var response = await _applicationDbContext.socialMedias.Where(x => x.Id == request.id).FirstOrDefaultAsync();

            if (response == null)
            {
                return new UpdateSocialMediaResponse
                {
                    response = 500,
                    status = false,
                    message = "Records Not Found"
                };
            }

            response.SocialMediaProfile = null;
             _applicationDbContext.socialMedias.Update(response);
            await _applicationDbContext.SaveChangesAsync();
            return new UpdateSocialMediaResponse
            {
                response = 200,
                status = true,
                message = "Records remove Sucessfully"
            };
        }

        public async Task<UpdateSocialMediaResponse> DeleteSocialMedia(DeleteSocialMedia request)
        {
            var response = await _applicationDbContext.socialMedias.Where(x => x.Id == request.id).FirstOrDefaultAsync();
            if(response == null)
            {
                return new UpdateSocialMediaResponse
                {
                    response = 500,
                    status = false,
                    message = "Records Not Found"
                };
                
            }
            response.IsDeleted = true;
            _applicationDbContext.socialMedias.Update(response);
            await _applicationDbContext.SaveChangesAsync();
            return new UpdateSocialMediaResponse
            {
                response = 200,
                status = true,
                message = "Records Deleted Sucessfully"
            };
        }

        public async Task<List<SocialMediaDto>> GetSocialMediaAsync()
        {
            return await _applicationDbContext.socialMedias
                .Where(x => !x.IsDeleted)
                .Select(x => new SocialMediaDto
                {
                    id = x.Id,
                    title = x.Title,
                    image = x.SocialMediaProfile,
                    url = x.Url,
                    createdAt = x.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    updatedAt = x.LastModificationTime.ToString()
                })
                .ToListAsync();
        }
    }
}
