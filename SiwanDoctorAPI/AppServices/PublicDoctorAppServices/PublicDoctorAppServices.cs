using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SiwanDoctorAPI.DbConnection;
using SiwanDoctorAPI.Model.InputDTOModel.DoctorInputDTO;
using static SiwanDoctorAPI.DbConnection.ApplicationDbContext;

namespace SiwanDoctorAPI.AppServices.PublicDoctorAppServices
{
    public class PublicDoctorAppServices : IPublicDoctorAppServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _applicationDbContext;
        public PublicDoctorAppServices(UserManager<ApplicationUser> userManager, IConfiguration configuration, ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _configuration = configuration;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<GetDoctorResponse> GetDoctorsAsync()
        {
            var response = new GetDoctorResponse();

            var doctors = await _applicationDbContext.Doctor_Details.Where(x => x.IsDeleted == false)
                .ToListAsync();

            if (doctors == null || !doctors.Any())
            {
                response.response = 404;
                response.data = null;
                return response;
            }

            response.response = 200;
            response.data = doctors.Select(doctor => new DoctorDTO
            {
                id = doctor.Id,
                //stop_booking = doctor.StopBooking,
                user_id = doctor.UserId,
                //department = doctor.Department.ToString(),
                description = doctor.Description,
                specialization = doctor.Specialization,
                ex_year = doctor.ExperienceYears,
                //active = doctor.IsActive,
                //video_appointment = doctor.VideoAppointment,
                //clinic_appointment = doctor.ClinicAppointment,
                //emergency_appointment = doctor.EmergencyAppointment,
                opd_fee = doctor.OpdFee,
                video_fee = doctor.VideoFee,
                emg_fee = doctor.EmergencyFee,
                zoom_client_id = doctor.ZoomClientId,
                zoom_secret_id = doctor.ZoomSecretId,
                insta_link = doctor.InstagramLink,
                fb_linik = doctor.FacebookLink,
                twitter_link = doctor.TwitterLink,
                you_tube_link = doctor.YouTubeLink,
                created_at = doctor.CreationTime,
                //updated_at = doctor.LastModificationTime.ToString(),
                f_name = doctor.User?.FirstName,
                l_name = doctor.User?.LastName,
                phone = doctor.User?.PhoneNumber,
                isd_code = doctor.User?.ISDCode,
                gender = doctor.User?.Gender,
                //dob = doctor.DateOfBirth.ToString(),
                email = doctor.User?.Email,
                image = doctor.ProfileImagePath,
                department_name = doctor.Department,
                //total_review_points = doctor.TotalReviewPoints,
                //number_of_reviews = doctor.NumberOfReviews,
                //average_rating = doctor.AverageRating.ToString("0.00"),
                //total_appointment_done = doctor.TotalAppointmentDone
            }).ToList();

            return response;
        }

        public async Task<GetDoctorResponse> GetDoctorByIdAsync(int id)
        {
            var response = new GetDoctorResponse();

            var doctor = await _applicationDbContext.Doctor_Details
                .FirstOrDefaultAsync(d => d.Id == id && d.IsDeleted ==false);

            if (doctor == null)
            {
                response.response = 404;
                response.data = null;
                return response;
            }

            response.response = 200;
            response.data = new List<DoctorDTO>
            {
                new DoctorDTO
                {
                    id = doctor.Id,
                        //stop_booking = doctor.StopBooking,
                        user_id = doctor.UserId,
                        //department = doctor.Department.ToString(),
                        description = doctor.Description,
                        specialization = doctor.Specialization,
                        ex_year = doctor.ExperienceYears,
                        //active = doctor.IsActive,
                        //video_appointment = doctor.VideoAppointment,
                        //clinic_appointment = doctor.ClinicAppointment,
                        //emergency_appointment = doctor.EmergencyAppointment,
                        opd_fee = doctor.OpdFee,
                        video_fee = doctor.VideoFee,
                        emg_fee = doctor.EmergencyFee,
                        zoom_client_id = doctor.ZoomClientId,
                        zoom_secret_id = doctor.ZoomSecretId,
                        insta_link = doctor.InstagramLink,
                        fb_linik = doctor.FacebookLink,
                        twitter_link = doctor.TwitterLink,
                        you_tube_link = doctor.YouTubeLink,
                        created_at = doctor.CreationTime,
                        //updated_at = doctor.LastModificationTime.ToString(),
                        f_name = doctor.User?.FirstName,
                        l_name = doctor.User?.LastName,
                        phone = doctor.User?.PhoneNumber,
                        isd_code = doctor.User?.ISDCode,
                        gender = doctor.User?.Gender,
                        //dob = doctor.DateOfBirth.ToString(),
                        email = doctor.User?.Email,
                        image = doctor.ProfileImagePath,
                        department_name = doctor.Department,
                        //total_review_points = doctor.TotalReviewPoints,
                        //number_of_reviews = doctor.NumberOfReviews,
                        //average_rating = doctor.AverageRating.ToString("0.00"),
                        //total_appointment_done = doctor.TotalAppointmentDone
                }
            };

            return response;
        }
    }
}
