namespace SiwanDoctorAPI.Model.InputDTOModel.FamilyMemberVitalsInoutDTO
{
    public class GetVitalsModel
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int family_member_id { get; set; }
        public int? bp_systolic { get; set; }
        public int? bp_diastolic { get; set; }
        public int? weight { get; set; }
        public int? spo2 { get; set; }
        public int? temperature { get; set; }
        public int? sugar_random { get; set; }
        public int? sugar_fasting { get; set; }
        public string? type { get; set; }
        public string? date { get; set; }
        public string? time { get; set; }
        public string? created_at { get; set; }
        public string? updated_at { get; set; }
    }
}
