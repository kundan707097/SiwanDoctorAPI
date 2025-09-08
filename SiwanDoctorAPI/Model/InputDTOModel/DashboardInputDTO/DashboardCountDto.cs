namespace SiwanDoctorAPI.Model.InputDTOModel.DashboardInputDTO
{
    public class DashboardCountDto
    {
        public string today_date { get; set; }
        public int total_today_appointment { get; set; }
        public int total_appointments { get; set; }
        public int total_pending_appointment { get; set; }
        public int total_confirmed_appointment { get; set; }
        public int total_rejected_appointment { get; set; }
        public int total_cancelled_appointment { get; set; }
        public int total_completed_appointment { get; set; }
        public int total_visited_appointment { get; set; }
        public int total_upcoming_appointments { get; set; }
        public int total_prescriptions { get; set; }
        public int total_medicine { get; set; }
        public int total_doctors_review { get; set; }
        public int total_cancel_req_initiated_appointment { get; set; }
        public int total_cancel_req_rejected_appointment { get; set; }
        public int total_cancel_req_approved_appointment { get; set; }
        public int total_cancel_req_processing_appointment { get; set; }
    }

    public class DashboardResponse
    {
        public int response { get; set; }
        public DashboardData data { get; set; }
    }

    public class DashboardData
    {
        public string today_date { get; set; }
        public int total_users { get; set; }
        public int total_patients { get; set; }
        public int total_today_appointment { get; set; }
        public int total_appointments { get; set; }
        public int total_active_doctors { get; set; }
        public int total_pending_appointment { get; set; }
        public int total_confirmed_appointment { get; set; }
        public int total_rejected_appointment { get; set; }
        public int total_cancelled_appointment { get; set; }
        public int total_completed_appointment { get; set; }
        public int total_visited_appointment { get; set; }
        public int total_upcoming_appointments { get; set; }
        public int total_departments { get; set; }
        public int total_prescriptions { get; set; }
        public int total_family_memebers { get; set; }
        public int total_medicine { get; set; }
        public int total_doctors_review { get; set; }
        public int total_cancel_req_initiated_appointment { get; set; }
        public int total_cancel_req_rejected_appointment { get; set; }
        public int total_cancel_req_approved_appointment { get; set; }
        public int total_cancel_req_processing_appointment { get; set; }
    }

}
