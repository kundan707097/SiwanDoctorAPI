using System.ComponentModel.DataAnnotations;

namespace SiwanDoctorAPI.Model.InputDTOModel.LoginInputDTO
{
    public class LoginInput
    {
        [Required]
        public string? emailOrPhoneNumber { get; set; }
        [Required]
        public string? password { get; set; }
    }
}
