namespace SiwanDoctorAPI.Model.InputDTOModel.DepartmentInputDTO
{
    public class RemoveDepartmentImageResponse
    {
        public int response { get; set; }   // HTTP Status Code (e.g., 200, 400, 404)
        public bool status { get; set; }    // Indicates success (true) or failure (false)
        public string? message { get; set; }
    }
}
