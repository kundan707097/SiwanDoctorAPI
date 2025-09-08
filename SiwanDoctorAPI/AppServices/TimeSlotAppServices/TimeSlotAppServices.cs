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
                    response.response = 201; 
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
                response.response = 200; 
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


        public async Task<ServiceResponse> AddVideoTimeSlotAsync(VideoTimeSlotRequest request)
        {
            try
            {
                // Validate input data
                if (string.IsNullOrEmpty(request.time_start) || string.IsNullOrEmpty(request.time_end) || string.IsNullOrEmpty(request.day))
                {
                    return new ServiceResponse
                    {
                        response = 400,
                        status = false,
                        message = "Invalid input data"
                    };
                }

                // Convert TimeDuration to int safely
                //if (!int.TryParse(request.time_duration, out int duration))
                //{
                //    return new ServiceResponse
                //    {
                //        response = 400,
                //        status = false,
                //        message = "Invalid time duration"
                //    };
                //}

                // Create a new timeslot entity
                var timeSlot = new VideoDoctorTimeSlot
                {
                    doct_id = request.doct_id,
                    TimeStart = request.time_start,
                    TimeEnd = request.time_end,
                    TimeDuration = request.time_duration,
                    Day = request.day,
                    CreationTime = DateTime.UtcNow
                };

                // Add to the database
                _applicationDbContext.videoDoctorTimeSlots.Add(timeSlot);
                await _applicationDbContext.SaveChangesAsync();

                return new ServiceResponse
                {
                    response = 200,
                    status = true,
                    message = "Video created successfully"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse
                {
                    response = 500,
                    status = false,
                    message = $"Internal Server Error: {ex.Message}"
                };
            }
        }


        public async Task<ServiceResponse> DeleteVideoTimeSlotAsync(DeleteVideoTimeSlotRequest request)
        {
            try
            {
                // Check if the ID is valid and exists in the database
                var timeSlot = await _applicationDbContext.videoDoctorTimeSlots
                    .FirstOrDefaultAsync(ts => ts.Id.ToString() == request.id);

                if (timeSlot == null)
                {
                    return new ServiceResponse
                    {
                        response = 400,
                        status = false,
                        message = "Time slot not found"
                    };
                }

                // Delete the time slot
                timeSlot.IsDeleted = true;
                _applicationDbContext.videoDoctorTimeSlots.Update(timeSlot);
                await _applicationDbContext.SaveChangesAsync();

                return new ServiceResponse
                {
                    response = 200,
                    status = true,
                    message = "Slot deleted successfully"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse
                {
                    response = 500,
                    status = false,
                    message = $"Internal Server Error: {ex.Message}"
                };
            }
        }

        public async Task<ServiceResponse> DeleteTimeSlotAsync(DeleteTimeSlotRequest request)
        {
            try
            {
                var timeSlot = await _applicationDbContext.Doctor_TimeSlots
                    .FirstOrDefaultAsync(ts => ts.Id.ToString() == request.id);

                if (timeSlot == null)
                {
                    return new ServiceResponse
                    {
                        response = 400,
                        status = false,
                        message = "Time slot not found"
                    };
                }

                // Delete the time slot
                timeSlot.IsDeleted = true;
                _applicationDbContext.Doctor_TimeSlots.Update(timeSlot);
                await _applicationDbContext.SaveChangesAsync();

                return new ServiceResponse
                {
                    response = 200,
                    status = true,
                    message = "Slot deleted successfully"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse
                {
                    response = 500,
                    status = false,
                    message = $"Internal Server Error: {ex.Message}"
                };
            }
        }
    }
}
