namespace SiwanDoctorAPI.Model.InputDTOModel.AdminInputDTO
{
    public class UserDto
    {
        public int id { get; set; }
        public decimal wallet_amount { get; set; }
        public string? f_name { get; set; }
        public string? l_name { get; set; }
        public string? phone { get; set; }
        public string? isd_code { get; set; }
        public string? gender { get; set; }
        public DateTime? dob { get; set; }
        public string? email { get; set; }
        public string? image { get; set; }
        public string? address { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? postal_code { get; set; }
        public string? isd_code_sec { get; set; }
        public string? phone_sec { get; set; }
        public DateTime? email_verified_at { get; set; }
        public string? remember_token { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
