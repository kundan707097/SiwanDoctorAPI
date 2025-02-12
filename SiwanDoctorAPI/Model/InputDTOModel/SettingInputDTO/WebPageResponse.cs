namespace SiwanDoctorAPI.Model.InputDTOModel.SettingInputDTO
{
    public class WebPageResponse
    {
        public int id { get; set; }
        public int pageId { get; set; }
        public string? title { get; set; }
        public string? body { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
