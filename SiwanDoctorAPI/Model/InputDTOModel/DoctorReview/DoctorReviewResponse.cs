namespace SiwanDoctorAPI.Model.InputDTOModel.DoctorReview
{
    public class DoctorReviewResponse
    {
        public int totalReviewPoints { get; set; }
        public int numberOfReviews { get; set; }
        public string? averageRating { get; set; }
        public int response { get; set; }
        public List<DoctorReviewData> data { get; set; }
    }

    public class DoctorReviewData
    {
        public int id { get; set; }
        public int doctor_id { get; set; }
        public int user_id { get; set; }
        public int appointment_id { get; set; }
        public double points { get; set; }
        public string? description { get; set; }
        public string? created_at { get; set; }
        public string? updated_at { get; set; }
        public string? f_name { get; set; }
        public string? l_name { get; set; }
        public string? doct_f_name { get; set; }
        public string? doct_l_name { get; set; }
    }
}
