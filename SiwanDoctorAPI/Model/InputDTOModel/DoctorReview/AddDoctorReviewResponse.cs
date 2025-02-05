namespace SiwanDoctorAPI.Model.InputDTOModel.DoctorReview
{
    public class AddDoctorReviewResponse
    {
        public int response { get; set; } = 200;
        public bool status { get; set; }
        public string? message { get; set; }
        public int id { get; set; }
    }
}
