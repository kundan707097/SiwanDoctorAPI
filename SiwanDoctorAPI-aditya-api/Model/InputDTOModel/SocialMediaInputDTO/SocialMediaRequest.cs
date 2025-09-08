namespace SiwanDoctorAPI.Model.InputDTOModel.SocialMediaInputDTO
{
    public class SocialMediaRequest
    {
        public string? title { get; set; }
        public IFormFile? image { get; set; }
        public string? url { get; set; }
    }
}
