namespace SiwanDoctorAPI.Model.InputDTOModel.DepartmentInputDTO
{
    public class DeleteDepartmentResponse
    {
        public int response { get; set; } = 201;
        public bool status { get; set; }
        public string? message { get; set; }
    }
}
