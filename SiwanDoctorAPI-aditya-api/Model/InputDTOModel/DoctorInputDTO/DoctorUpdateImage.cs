namespace SiwanDoctorAPI.Model.InputDTOModel.DoctorInputDTO
{
    public class DoctorUpdateImage
    {
        public int id {  get; set; }

        public IFormFile? image { get; set; }
    }
}
