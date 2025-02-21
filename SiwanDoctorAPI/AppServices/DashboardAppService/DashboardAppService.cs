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


        //public async Task<DashboardCountDto> GetDashboardCountAsync(int doctorId)
        //{
        //    var today = DateTime.UtcNow.Date;
        //    var appointments = _applicationDbContext.appointments.Where(a => a.FK_DoctId == doctorId);

        //    var result = new DashboardCountDto
        //    {
        //        today_date = today.ToString("yyyy-MM-dd"),
        //        total_today_appointment = await appointments.CountAsync(a => a.Date == today),
        //        total_appointments = await appointments.CountAsync(),
        //        total_pending_appointment = await appointments.CountAsync(a => a.Status == "Pending"),
        //        total_confirmed_appointment = await appointments.CountAsync(a => a.Status == "Confirmed"),
        //        total_rejected_appointment = await appointments.CountAsync(a => a.Status == "Rejected"),
        //        total_cancelled_appointment = await appointments.CountAsync(a => a.Status == "Cancelled"),
        //        total_completed_appointment = await appointments.CountAsync(a => a.Status == "Completed"),
        //        total_visited_appointment = await appointments.CountAsync(a => a.Status == "Visited"),
        //        total_upcoming_appointments = await appointments.CountAsync(a => a.Date > today),
        //        total_prescriptions = await _context.Prescriptions.CountAsync(p => p.DoctorId == doctorId),
        //        total_medicine = await _context.Medicines.CountAsync(m => m.DoctorId == doctorId),
        //        total_doctors_review = await _context.Reviews.CountAsync(r => r.DoctorId == doctorId),
        //        total_cancel_req_initiated_appointment = await appointments.CountAsync(a => a.CancelRequestStatus == "Initiated"),
        //        total_cancel_req_rejected_appointment = await appointments.CountAsync(a => a.CancelRequestStatus == "Rejected"),
        //        total_cancel_req_approved_appointment = await appointments.CountAsync(a => a.CancelRequestStatus == "Approved"),
        //        total_cancel_req_processing_appointment = await appointments.CountAsync(a => a.CancelRequestStatus == "Processing")
        //    };

        //    return result;
        //}
    }
}
