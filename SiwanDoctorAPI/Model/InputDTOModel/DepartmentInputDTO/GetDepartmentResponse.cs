namespace SiwanDoctorAPI.Model.InputDTOModel.DepartmentInputDTO
{
    public class GetDepartmentResponse
    {
        public int response { get; set; } = 200;
        public List<DepartmentDto> data { get; set; }
    }

    public class DepartmentDto
    {
        public int id { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public string? image { get; set; }
        public int active { get; set; }
        public string? createdAt { get; set; }
        public string? updatedAt { get; set; }
    }
}
