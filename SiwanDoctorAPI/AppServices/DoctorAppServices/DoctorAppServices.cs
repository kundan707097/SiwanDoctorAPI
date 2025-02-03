using Microsoft.AspNetCore.Identity;
using SiwanDoctorAPI.DbConnection;
using static SiwanDoctorAPI.DbConnection.ApplicationDbContext;

namespace SiwanDoctorAPI.AppServices.DoctorAppServices
{
    public class DoctorAppServices : IDoctorAppServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _applicationDbContext;
        public DoctorAppServices(UserManager<ApplicationUser> userManager, IConfiguration configuration, ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _configuration = configuration;
            _applicationDbContext = applicationDbContext;


        }



    }
}
