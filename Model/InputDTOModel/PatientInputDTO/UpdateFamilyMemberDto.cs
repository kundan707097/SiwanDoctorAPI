namespace SiwanDoctorAPI.Model.InputDTOModel.PatientInputDTO
{
    public class UpdateFamilyMemberDto
    {
        public int id { get; set; }
        public string? f_name { get; set; }
        public string? l_name { get; set; }
        public string? phone { get; set; }
        public string? isd_code { get; set; }
        public string? gender { get; set; }
        public string? dob { get; set; }
    }
}
