namespace SiwanDoctorAPI.Model.InputDTOModel.LoginInputDTO
{
    public class UpdateUserRoleRequest
    {
        public string UserId { get; set; }
        public string NewRole { get; set; }
    }
    public class CreateRoleRequest
    {
        public string RoleName { get; set; }
    }
}
