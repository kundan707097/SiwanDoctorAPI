namespace SiwanDoctorAPI.Model.InputDTOModel.PatientInputDTO
{
    public class GetFamilyMemberResponse
    {
        public int id { get; set; }
        public string? f_name { get; set; }
        public string? l_name { get; set; }
        public string? phone { get; set; }
        public string? isd_code { get; set; }
        public int user_id { get; set; }
        public string? gender { get; set; }
        public DateTime dob { get; set; }
        public string? created_at { get; set; }
        public string? updated_at { get;set; }
    }
}
