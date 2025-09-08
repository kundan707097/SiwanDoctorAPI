namespace SiwanDoctorAPI.Model.InputDTOModel.DoctorReview
{
    public class AddDoctorReviewRequest
    {
        public int doctor_id { get; set; }
        public double points { get; set; }
        public string? description { get; set; }
        public int user_id { get; set; }
        public int appointment_id { get; set; }
    }
}
