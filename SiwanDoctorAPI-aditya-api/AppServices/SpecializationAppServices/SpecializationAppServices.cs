using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SiwanDoctorAPI.DbConnection;
using SiwanDoctorAPI.Model.EntityModel.DoctorEntity;
using SiwanDoctorAPI.Model.InputDTOModel.SpecializationInputDTO;
using static SiwanDoctorAPI.DbConnection.ApplicationDbContext;

namespace SiwanDoctorAPI.AppServices.SpecializationAppServices
{
    public class SpecializationAppServices : ISpecializationAppServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _applicationDbContext;
        public SpecializationAppServices(UserManager<ApplicationUser> userManager, IConfiguration configuration, ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _configuration = configuration;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<int> AddSpecializationAsync(SpecializationDto specializationDto)
        {
            var specialization = new Specialization { Title = specializationDto.title };
            await _applicationDbContext.Doctor_Specializations.AddAsync(specialization);
            var result = await _applicationDbContext.SaveChangesAsync();
            return result > 0 ? specialization.Id : 0;
        }

        public async Task<bool> UpdateSpecializationAsync(UpdateSpecializationInputDTO request)
        {
            var specialization = await _applicationDbContext.Doctor_Specializations.FindAsync(request.id);
            if (specialization == null)
                return false;

            specialization.Title = request.title;

            _applicationDbContext.Doctor_Specializations.Update(specialization);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SoftDeleteSpecializationAsync(int id)
        {
            var specialization = await _applicationDbContext.Doctor_Specializations.FindAsync(id);
            if (specialization == null)
                return false;

            specialization.IsDeleted = true;  // Set the IsDeleted flag to true
            _applicationDbContext.Doctor_Specializations.Update(specialization);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }


        public async Task<List<GetAllSpecializationResponse>> GetSpecializationsAsync()
        {
            return await _applicationDbContext.Doctor_Specializations
                .Where(s => !s.IsDeleted)  // Fetch only active specializations
                .Select(s => new GetAllSpecializationResponse
                {
                    id = s.Id,
                    title = s.Title,
                    createdAt = s.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),  // Format date
                    updatedAt = s.LastModificationTime.ToString()
                })
                .ToListAsync();
        }

        public async Task<GetAllSpecializationResponse> GetSpecializationByIdAsync(int id)
        {
            var specialization = await _applicationDbContext.Doctor_Specializations
            .Where(s => s.Id == id && !s.IsDeleted)  // Exclude deleted records
            .Select(s => new GetAllSpecializationResponse
            {
                id = s.Id,
                title = s.Title,
                createdAt = s.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                updatedAt = s.LastModificationTime.ToString()
            })
            .FirstOrDefaultAsync();

            if (specialization == null)
            {
                return null;  
            }

            return specialization;
        }
    }
}
