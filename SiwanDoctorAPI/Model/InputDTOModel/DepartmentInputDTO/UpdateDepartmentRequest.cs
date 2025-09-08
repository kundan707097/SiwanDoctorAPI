namespace SiwanDoctorAPI.Model.InputDTOModel.DepartmentInputDTO
{
    public class UpdateDepartmentRequest
    {
        public int id {  get; set; }
        public string? title { get; set; }
        public string? description { get; set; }

        public IFormFile? image { get; set; }
        public string? active { get; set; }
    }
}
