using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SiwanDoctorAPI.DbConnection;
using SiwanDoctorAPI.Model.EntityModel.DoctorEntity;
using SiwanDoctorAPI.Model.InputDTOModel.DoctorInputDTO;
using SiwanDoctorAPI.Model.InputDTOModel.TimeSlotInputDTO;
using static SiwanDoctorAPI.DbConnection.ApplicationDbContext;

namespace SiwanDoctorAPI.AppServices.TimeSlotAppServices
{
    public class TimeSlotAppServices: ITimeSlotAppServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _applicationDbContext;
        public TimeSlotAppServices(UserManager<ApplicationUser> userManager, IConfiguration configuration, ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _configuration = configuration;
            _applicationDbContext = applicationDbContext;
        }
        public async Task<TimeSlotResponse> AddTimeSlotAsync(AddTimeSlotRequest request)
        {
            var response = new TimeSlotResponse();

            try
            {
                // Check if the doctor exists
                var doctor = await _applicationDbContext.Doctor_Details.FindAsync(request.doct_id);
                if (doctor == null)
                {
                    response.response = 404;
                    response.status = false;
                    response.message = "Doctor not found";
                    return response;
                }

                // Convert time strings to TimeSpan for proper time comparison
                TimeSpan startTime = TimeSpan.Parse(request.time_start);
                TimeSpan endTime = TimeSpan.Parse(request.time_end);

                // Check if a time slot already exists for the same doctor and same day with conflicting times
                bool isConflict = _applicationDbContext.Doctor_TimeSlots
                    .AsEnumerable() // Forces evaluation in memory
                    .Any(slot =>
                        slot.doct_id == request.doct_id &&
                        slot.Day == request.day &&
                        (
                            (startTime >= TimeSpan.Parse(slot.TimeStart) && startTime < TimeSpan.Parse(slot.TimeEnd)) ||
                            (endTime > TimeSpan.Parse(slot.TimeStart) && endTime <= TimeSpan.Parse(slot.TimeEnd)) ||
                            (startTime <= TimeSpan.Parse(slot.TimeStart) && endTime >= TimeSpan.Parse(slot.TimeEnd))
                        )
                    );


                if (isConflict)
                {
                    response.response = 409; 
                    response.status = false;
                    response.message = "Time slot already exists or overlaps with another slot.";
                    return response;
                }

                var timeSlot = new DoctorTimeSlot
                {
                    doct_id = request.doct_id,
                    TimeStart = request.time_start,
                    TimeEnd = request.time_end, // Fixing incorrect assignment in previous code
                    TimeDuration = request.time_duration.ToString(),
                    Day = request.day
                };

                _applicationDbContext.Doctor_TimeSlots.Add(timeSlot);
                await _applicationDbContext.SaveChangesAsync();

                // Successful response
                response.response = 201; 
                response.status = true;
                response.message = "Time slot added successfully";
            }
            catch (Exception ex)
            {
                response.response = 500; // HTTP 500 Internal Server Error
                response.status = false;
                response.message = $"An error occurred: {ex.Message}";
            }

            return response;
        }



    }
}
