using Microsoft.AspNetCore.Mvc;

namespace SiwanDoctorAPI.Model.InputDTOModel.UpdateInputDTO
{
    public class UpdatePatientModel
    {
        public string? Id { get; set; }
        public string? email {  get; set; }
        public string? f_name { get; set; }
        public string? l_last {  get; set; }

        public string? isd_code {  get; set; }
        public string? phone {  get; set; }
        public string? gender {  get; set; }

        public IFormFile? image {  get; set; }
        
    }
}
