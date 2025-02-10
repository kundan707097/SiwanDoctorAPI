namespace SiwanDoctorAPI.Model.InputDTOModel.AppointmentInputDTO
{

    public class AppointmentResponseByappointmentId
    {
        public int response { get; set; }
        public AppointmentDataByappointmentId data { get; set; }
    }
    public class AppointmentDataByappointmentId
    {
        public int id { get; set; }
        public int patient_id { get; set; }
        public string status { get; set; }
        public string date { get; set; }
        public string time_slots { get; set; }
        public int doct_id { get; set; }
        public int dept_id { get; set; }
        public string type { get; set; }
        public string? meeting_id { get; set; }
        public string? meeting_link { get; set; }
        public string payment_status { get; set; }
        public string? current_cancel_req_status { get; set; }
        public string? source { get; set; }
        public string? created_at { get; set; }
        public string? updated_at { get; set; }
        public int user_id { get; set; }
        public string? patient_f_name { get; set; }
        public string? patient_l_name { get; set; }
        public string? patient_phone { get; set; }
        public string? patient_gender { get; set; }
        public string? dept_title { get; set; }
        public string? doct_f_name { get; set; }
        public string? doct_l_name { get; set; }
        public string? doct_image { get; set; }
        public string? doct_specialization { get; set; }
        public int total_review_points { get; set; }
        public int number_of_reviews { get; set; }
        public string? average_rating { get; set; }
        public int total_appointment_done { get; set; }
    }
}
