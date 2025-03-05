namespace SiwanDoctorAPI.Model.InputDTOModel.DoctorInputDTO
{
    public class UpdateDoctorDTO
    {
        public int id { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public string? f_name { get; set; }
        public string? l_name { get; set; }
        public int department { get; set; }
        public int ex_year { get; set; }
        public bool active { get; set; }
        public string? isd_code { get; set; }
        public string? phone { get; set; }
        public string? isd_code_sec { get; set; }
        public string? phone_sec { get; set; }
        public string? description { get; set; }
        public string? gender { get; set; }
        public DateTime dob { get; set; }
        public IFormFile image { get; set; }
        public string? specialization { get; set; }
        public string? address { get; set; }
        public string? state { get; set; }
        public string? city { get; set; }
        public string? postal_code { get; set; }
        public string? zoom_client_id { get; set; }
        public string? zoom_secret_id { get; set; }
        public string? fb_linik { get; set; }
        public string? twitter_link { get; set; }
        public string? you_tube_link { get; set; }
        public string? insta_link { get; set; }
        public decimal? opd_fee { get; set; }
        public decimal? video_fee { get; set; }
        public decimal? emg_fee { get; set; }
    }
}
