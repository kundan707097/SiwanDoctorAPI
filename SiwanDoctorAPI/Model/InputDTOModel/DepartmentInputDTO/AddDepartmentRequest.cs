namespace SiwanDoctorAPI.Model.InputDTOModel.DepartmentInputDTO
{
    public class AddDepartmentRequest
    {
        public string? title { get; set; }
        public string? description { get; set; }
        public IFormFile image { get; set; }
    }
}
