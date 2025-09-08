using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SiwanDoctorAPI.DbConnection;
using SiwanDoctorAPI.Model.EntityModel.DoctorEntity;
using SiwanDoctorAPI.Model.InputDTOModel.DoctorInputDTO;
using SiwanDoctorAPI.Model.InputDTOModel.PublicInputDTO;
using SiwanDoctorAPI.Model.InputDTOModel.TimeSlotInputDTO;
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

            var doctors = await _applicationDbContext.Doctor_Details
                .Where(x => !x.IsDeleted)
                .Include(d => d.department) // Include department details
                .Include(d => d.DoctorReview) // Ensure this is a collection
                .ToListAsync();

            if (doctors == null || !doctors.Any())
            {
                response.response = 404;
                response.data = null;
                return response;
            }

            response.response = 200;
            response.data = doctors.Select(d => new DoctorDTO
            {
                id = d.Id,
                stop_booking = d.StopBooking,
                user_id = d.UserId,
                department = d.DepartmentId,
                description = d.Description,
                specialization = d.Specialization,
                ex_year = d.ExperienceYears,
                active = d.IsActive,
                video_appointment = d.VideoAppointment ? 1 : 0,
                clinic_appointment = d.ClinicAppointment ? 1 : 0,
                emergency_appointment = d.EmergencyAppointment ? 1 : 0,
                opd_fee = d.OpdFee,
                video_fee = d.VideoFee,
                emg_fee = d.EmergencyFee,
                zoom_client_id = d.ZoomClientId,
                zoom_secret_id = d.ZoomSecretId,
                insta_link = d.InstagramLink,
                fb_linik = d.FacebookLink,
                twitter_link = d.TwitterLink,
                you_tube_link = d.YouTubeLink,
                created_at = d.CreationTime,
                f_name = d.FirstName,
                l_name = d.LastName,
                phone = d.Phone,
                isd_code = d.ISDCode,
                gender = d.Gender,
                dob = d.DateOfBirth ?? DateTime.MinValue,
                email = d.Email,
                image = d.ProfileImagePath,
                address = d.doctor_Address,
                city = d.city,
                postal_code = d.PostalCode,
                state = d.State,
                department_name = d.department?.Title,

                total_review_points = d.DoctorReview != null ? d.DoctorReview.Sum(r => r.Points) : 0, // Ensure it's a collection
                number_of_reviews = d.DoctorReview?.Count() ?? 0, // Count the number of reviews
                average_rating = (d.DoctorReview != null && d.DoctorReview.Any())
                    ? d.DoctorReview.Average(r => r.Points).ToString("0.00")
                    : "0.00", // Average review points with proper check

                total_appointment_done = d.DoctorReview != null
                    ? d.DoctorReview.Select(r => r.AppointmentId).Distinct().Count()
                    : 0 // Ensure we get a unique appointment count
            }).ToList();

            return response;
        }

        public async Task<GetDoctorResponse> GetDoctorByIdAsync(int id)
        {
            var response = new GetDoctorResponse();

            var doctor = await _applicationDbContext.Doctor_Details
                .Include(d => d.department)
                .FirstOrDefaultAsync(d => d.Id == id && d.IsDeleted == false);

            if (doctor == null)
            {
                response.response = 404;
                response.data = null;
                return response;
            }

            // Fetch doctor's reviews and calculate review stats
            var reviews = await _applicationDbContext.doctorReviews
                .Where(r => r.FK_DoctorId == id)
                .ToListAsync();

            double totalReviewPoints = reviews.Sum(r => r.Points);
            int numberOfReviews = reviews.Count;
            string averageRating = numberOfReviews > 0 ? (totalReviewPoints / numberOfReviews).ToString("0.00") : "0.00";

            response.response = 200;
            response.data = new List<DoctorDTO>
            {
                new DoctorDTO
                {
                    id = doctor.Id,
                    stop_booking= doctor.StopBooking,
                    user_id = doctor.UserId,
                    department = doctor.DepartmentId,
                    description = doctor.Description,
                    specialization = doctor.Specialization,
                    ex_year = doctor.ExperienceYears,
                    active = doctor.IsActive,
                    video_appointment = doctor.VideoAppointment ? 1 : 0,
                    clinic_appointment = doctor.ClinicAppointment ? 1 : 0,
                    emergency_appointment = doctor.EmergencyAppointment ? 1 : 0,
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
                    f_name = doctor.FirstName,
                    l_name = doctor.LastName,
                    phone = doctor.Phone,
                    isd_code = doctor.ISDCode,
                    gender = doctor.Gender,
                    dob = doctor.DateOfBirth ?? DateTime.MinValue,
                    email = doctor.Email,
                    image = doctor.ProfileImagePath,
                    address = doctor.doctor_Address,
                    city = doctor.city,
                    postal_code = doctor.PostalCode,
                    state = doctor.State,
                    department_name = doctor.department?.Title, // Assuming department has a Title
                    total_review_points = totalReviewPoints,
                    number_of_reviews = numberOfReviews,
                    average_rating = averageRating
                }
            };

            return response;
        }
        public async Task<GetDoctorResponse> GetDoctorByDepartmentIdAsync(int departmentId)
        {
            var response = new GetDoctorResponse();

            // Fetch all doctors in the department
            var doctors = await _applicationDbContext.Doctor_Details
                .Include(d => d.department)
                .Where(d => d.DepartmentId == departmentId && d.IsDeleted == false)
                .ToListAsync();

            if (doctors == null || doctors.Count == 0)
            {
                response.response = 404;
                response.data = null;
                return response;
            }

            var doctorDtos = new List<DoctorDTO>();

            foreach (var doctor in doctors)
            {
                // Get reviews for this doctor
                var reviews = await _applicationDbContext.doctorReviews
                    .Where(r => r.FK_DoctorId == doctor.Id)
                    .ToListAsync();

                double totalReviewPoints = reviews.Sum(r => r.Points);
                int numberOfReviews = reviews.Count;
                string averageRating = numberOfReviews > 0
                    ? (totalReviewPoints / numberOfReviews).ToString("0.00")
                    : "0.00";

                doctorDtos.Add(new DoctorDTO
                {
                    id = doctor.Id,
                    stop_booking = doctor.StopBooking,
                    user_id = doctor.UserId,
                    department = doctor.DepartmentId,
                    description = doctor.Description,
                    specialization = doctor.Specialization,
                    ex_year = doctor.ExperienceYears,
                    active = doctor.IsActive,
                    video_appointment = doctor.VideoAppointment ? 1 : 0,
                    clinic_appointment = doctor.ClinicAppointment ? 1 : 0,
                    emergency_appointment = doctor.EmergencyAppointment ? 1 : 0,
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
                    f_name = doctor.FirstName,
                    l_name = doctor.LastName,
                    phone = doctor.Phone,
                    isd_code = doctor.ISDCode,
                    gender = doctor.Gender,
                    dob = doctor.DateOfBirth ?? DateTime.MinValue,
                    email = doctor.Email,
                    image = doctor.ProfileImagePath,
                    address = doctor.doctor_Address,
                    city = doctor.city,
                    postal_code = doctor.PostalCode,
                    state = doctor.State,
                    department_name = doctor.department?.Title,
                    total_review_points = totalReviewPoints,
                    number_of_reviews = numberOfReviews,
                    average_rating = averageRating
                });
            }

            response.response = 200;
            response.data = doctorDtos;

            return response;
        }


        public async Task<List<ListDoctorTimeSlot>> GetDoctorTimeSlotsAsync(int doctorId)
        {
            var records = await _applicationDbContext.Doctor_TimeSlots
                .Where(t => t.doct_id == doctorId && t.IsDeleted ==false)
                .ToListAsync(); // Fetch data first

            return records.Select(t => new ListDoctorTimeSlot
            {
                id = t.Id,
                doct_Id = t.doct_id,
                time_start = t.TimeStart,
                time_end = t.TimeEnd,
                time_duration = int.TryParse(t.TimeDuration, out int duration) ? duration : 0,  // Convert here
                day = t.Day,
                created_at = t.CreationTime.ToString(),
                updated_at = t.LastModificationTime.ToString(),
            }).ToList();
        }

        public async Task<List<DoctorTimeIntervalDTO>> GetDoctorTimeIntervalsAsync(int doctorId, string day)
        {
            var doctorTimeSlots = await _applicationDbContext.Doctor_TimeSlots
            .Where(t => t.doct_id == doctorId && t.Day == day && t.IsDeleted ==false)
            .ToListAsync();
            if (!doctorTimeSlots.Any()) return new List<DoctorTimeIntervalDTO>();
            List<DoctorTimeIntervalDTO> timeIntervals = new List<DoctorTimeIntervalDTO>();

            foreach (var slot in doctorTimeSlots)
            {
                var startTime = TimeSpan.Parse(slot.TimeStart);
                var endTime = TimeSpan.Parse(slot.TimeEnd);
                int duration = int.Parse(slot.TimeDuration);

                while (startTime < endTime)
                {
                    var nextTime = startTime.Add(TimeSpan.FromMinutes(duration));

                    if (nextTime > endTime) break;

                    timeIntervals.Add(new DoctorTimeIntervalDTO
                    {
                        time_start = startTime.ToString(@"hh\:mm"),
                        time_end = nextTime.ToString(@"hh\:mm")
                    });

                    startTime = nextTime;
                }
            }
            return timeIntervals;
        }

        public async Task<List<ListDoctorTimeSlot>> GetDoctorVideoTimeSlotsAsync(int doctorId)
        {
            // Fetch the time slots for the given doctor ID
            var timeSlots = await _applicationDbContext.videoDoctorTimeSlots
                .Where(ts => ts.doct_id == doctorId && !ts.IsDeleted)  // Assuming IsDeleted is for soft deletes
                .OrderBy(ts => ts.Day)  // Optional ordering by day or time
                .ToListAsync();
            return timeSlots.Select(t => new ListDoctorTimeSlot
            {
                id = t.Id,
                doct_Id = t.doct_id,
                time_start = t.TimeStart,
                time_end = t.TimeEnd,
                time_duration = int.TryParse(t.TimeDuration, out int duration) ? duration : 0,  // Convert here
                day = t.Day,
                created_at = t.CreationTime.ToString(),
                updated_at = t.LastModificationTime.ToString(),
            }).ToList();

        }

        public async Task<List<DoctorTimeIntervalDTO>> GetVideoDoctorTimeIntervalAsync(int doctorId, string day)
        {
            var videoDoctorTimeSlots = await _applicationDbContext.videoDoctorTimeSlots.Where(vt=>vt.doct_id==doctorId && vt.IsDeleted ==false).ToListAsync();
            if (!videoDoctorTimeSlots.Any()) return new List<DoctorTimeIntervalDTO>();
            List<DoctorTimeIntervalDTO> timeIntervals = new List<DoctorTimeIntervalDTO>();
            foreach (var videoSlot in videoDoctorTimeSlots)
            {
                var startTime = TimeSpan.Parse(videoSlot.TimeStart);
                var endTime = TimeSpan.Parse(videoSlot.TimeEnd);
                int duration = int.Parse(videoSlot.TimeDuration);

                while(startTime < endTime)
                {
                    var nextTime = startTime.Add(TimeSpan.FromMinutes(duration));

                    if (nextTime > endTime) break;

                    timeIntervals.Add(new DoctorTimeIntervalDTO
                    {
                        time_start = startTime.ToString(@"hh\:mm"),
                        time_end = nextTime.ToString(@"hh\:mm")
                    });

                    startTime = nextTime;
                }
            }
            return timeIntervals;
        }
        public async Task<BookedTimeSlotByDoctorResponse> GetBookedTimeSlot(BookedTimeSlotByDoctorRequest request)
        {
            var response = new BookedTimeSlotByDoctorResponse();

            if (request == null || request.doct_id == 0)
            {
                response.response = 400; // Bad Request
                return response;
            }

            var appointments = await _applicationDbContext.appointments
                .Where(x => x.FK_DoctId == request.doct_id && x.Status == "Confirmed" &&
                            (request.type == null || x.Type == request.type) &&
                            (request.date == null || x.Date == request.date))
                .Select(x => new BookedTimeSlotResponse
                {
                    time_slots = x.TimeSlots,
                    date = x.Date,
                    type = x.Type,
                    appointment_id = x.Id
                })
                .ToListAsync();

            response.response = 200; // Success
            response.data = appointments;
            return response;
        }
    }
}
