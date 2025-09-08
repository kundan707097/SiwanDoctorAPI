using Microsoft.EntityFrameworkCore;
using SiwanDoctorAPI.DbConnection;
using SiwanDoctorAPI.Model.EntityModel.SettingEntity;

namespace SiwanDoctorAPI.AppServices.WebPageAppServices
{
    public class SettingAppServices : ISettingAppServices
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public SettingAppServices(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<WebPage>> GetAllWebPagesAsync()
        {
            return await _applicationDbContext.webPages.ToListAsync(); // Assuming WebPages is the DbSet
        }
    }
}
