using Microsoft.EntityFrameworkCore;
using SiwanDoctorAPI.DbConnection;
using SiwanDoctorAPI.Model.InputDTOModel.DashboardInputDTO;

namespace SiwanDoctorAPI.AppServices.DashboardAppService
{
    public class DashboardAppService : IDashboardAppService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public DashboardAppService(ApplicationDbContext applicationDbContext)
        {
            
            _applicationDbContext = applicationDbContext;
        }


        public async Task<DashboardCountDto> GetDashboardCountAsync(int doctorId)
        {
            string todayString = DateTime.UtcNow.Date.ToString("yyyy-MM-dd");
            DateTime todayDate = DateTime.UtcNow.Date;

            var appointments = await _applicationDbContext.appointments
                .Where(a => a.FK_DoctId == doctorId)
                .ToListAsync(); 

            int totalUpcomingAppointments = appointments
                .Count(a => DateTime.TryParse(a.Date, out DateTime appointmentDate) && appointmentDate > todayDate);

            var result = new DashboardCountDto
            {
                today_date = todayString,

                total_today_appointment = appointments.Count(a => a.Date == todayString),

                total_appointments = appointments.Count,
                total_pending_appointment = appointments.Count(a => a.Status == "Pending"),
                total_confirmed_appointment = appointments.Count(a => a.Status == "Confirmed"),
                total_rejected_appointment = appointments.Count(a => a.Status == "Rejected"),
                total_cancelled_appointment = appointments.Count(a => a.Status == "Cancelled"),
                total_completed_appointment = appointments.Count(a => a.Status == "Completed"),
                total_visited_appointment = appointments.Count(a => a.Status == "Visited"),

                total_upcoming_appointments = totalUpcomingAppointments,

                total_prescriptions = await _applicationDbContext.patientPrescriptions.CountAsync(p => p.DoctorId == doctorId),
                total_medicine = await _applicationDbContext.doctorPrescribeMdicines.CountAsync(m => m.FK_DoctId == doctorId),
                total_doctors_review = await _applicationDbContext.doctorReviews.CountAsync(r => r.FK_DoctorId == doctorId),
                total_cancel_req_initiated_appointment = appointments.Count(a => a.currentcancelreqstatus == "Initiated"),
                total_cancel_req_rejected_appointment = appointments.Count(a => a.currentcancelreqstatus == "Rejected"),
                total_cancel_req_approved_appointment = appointments.Count(a => a.currentcancelreqstatus == "Approved"),
                total_cancel_req_processing_appointment = appointments.Count(a => a.currentcancelreqstatus == "Processing")
            };

            return result;
        }

        public async Task<DashboardResponse> GetAdminDashboardCountAsync()
        {
            string todayString = DateTime.UtcNow.Date.ToString("yyyy-MM-dd");
            DateTime todayDate = DateTime.UtcNow.Date;
            var appointments = await _applicationDbContext.appointments
                .ToListAsync();

            int totalUpcomingAppointments = appointments
                .Count(a => DateTime.TryParse(a.Date, out DateTime appointmentDate) && appointmentDate > todayDate);
            var response = new DashboardResponse
            {
                response = 200,
                data = new DashboardData
                {
                    today_date = todayString,
                    total_users = await _applicationDbContext.Users.CountAsync(),
                    total_patients = await _applicationDbContext.Patients_Details.CountAsync(),
                    total_today_appointment = await _applicationDbContext.appointments.CountAsync(a => a.Date == todayString),
                    total_appointments = await _applicationDbContext.appointments.CountAsync(),
                    total_active_doctors = await _applicationDbContext.Doctor_Details.CountAsync(d => d.IsDeleted ==false),
                    total_pending_appointment = await _applicationDbContext.appointments.CountAsync(a => a.Status == "Pending"),
                    total_confirmed_appointment = await _applicationDbContext.appointments.CountAsync(a => a.Status == "Confirmed"),
                    total_rejected_appointment = await _applicationDbContext.appointments.CountAsync(a => a.Status == "Rejected"),
                    total_cancelled_appointment = await _applicationDbContext.appointments.CountAsync(a => a.Status == "Cancelled"),
                    total_completed_appointment = await _applicationDbContext.appointments.CountAsync(a => a.Status == "Completed"),
                    total_visited_appointment = await _applicationDbContext.appointments.CountAsync(a => a.Status == "Visited"),
                    total_upcoming_appointments = totalUpcomingAppointments,
                    total_departments = await _applicationDbContext.Doctor_Departments.CountAsync(),
                    total_prescriptions = await _applicationDbContext.patientPrescriptions.CountAsync(),
                    total_family_memebers = await _applicationDbContext.User_FamilyMembers.CountAsync(),
                    total_medicine = await _applicationDbContext.patientMedicines.CountAsync(),
                    total_doctors_review = await _applicationDbContext.doctorReviews.CountAsync(),
                    total_cancel_req_initiated_appointment = await _applicationDbContext.appointments.CountAsync(a => a.currentcancelreqstatus == "Initiated"),
                    total_cancel_req_rejected_appointment = await _applicationDbContext.appointments.CountAsync(a => a.currentcancelreqstatus == "Rejected"),
                    total_cancel_req_approved_appointment = await _applicationDbContext.appointments.CountAsync(a => a.currentcancelreqstatus == "Approved"),
                    total_cancel_req_processing_appointment = await _applicationDbContext.appointments.CountAsync(a => a.currentcancelreqstatus == "Processing")
                }
            };

            return response;
        }

    }


}
