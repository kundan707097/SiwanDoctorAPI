namespace SiwanDoctorAPI.Model.InputDTOModel.DepartmentInputDTO
{
    public class GetSingleDepartmentResponse
    {
        public int Response { get; set; } = 200;
        public DepartmentDto Data { get; set; }
    }
}
