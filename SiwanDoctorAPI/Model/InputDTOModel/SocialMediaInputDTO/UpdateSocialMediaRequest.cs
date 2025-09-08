namespace SiwanDoctorAPI.Model.InputDTOModel.SocialMediaInputDTO
{
    public class UpdateSocialMediaRequest
    {
        public int id { get; set; }
        public string? title { get; set; }
        public IFormFile? image { get; set; }
        public string? url { get; set; }
    }

    public class RemoveSocialMediaImage
    {
        public int id { get; set; }
        public IFormFile? image { get; set; }
    }

    public class DeleteSocialMedia
    {
        public int id { get; set; }
    }
}
