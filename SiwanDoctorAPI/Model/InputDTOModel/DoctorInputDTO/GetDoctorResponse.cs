namespace SiwanDoctorAPI.Model.InputDTOModel.DoctorInputDTO
{
    public class GetDoctorResponse
    {
        public int response { get; set; }
        public List<DoctorDTO> data { get; set; }
    }


    public class DoctorDTO
    {
        public int id { get; set; }
        public bool stop_booking { get; set; }
        public int user_id { get; set; }
        public int? department { get; set; }
        public string? description { get; set; }
        public string? specialization { get; set; }
        public int? ex_year { get; set; }
        public bool? active { get; set; }
        public int video_appointment { get; set; }
        public int clinic_appointment { get; set; }
        public int emergency_appointment { get; set; }
        public decimal? opd_fee { get; set; }
        public decimal? video_fee { get; set; }
        public decimal? emg_fee { get; set; }
        public string? zoom_client_id { get; set; }
        public string? zoom_secret_id { get; set; }
        public string? insta_link { get; set; }
        public string? fb_linik { get; set; }
        public string? twitter_link { get; set; }
        public string? you_tube_link { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string? f_name { get; set; }
        public string? l_name { get; set; }
        public string? phone { get; set; }
        public string? isd_code { get; set; }
        public string? gender { get; set; }
        public DateTime dob { get; set; }
        public string? email { get; set; }
        public string? image { get; set; }
        public string? department_name { get; set; }
        public double total_review_points { get; set; }
        public int number_of_reviews { get; set; }
        public string? average_rating { get; set; }
        public int total_appointment_done { get; set; }

        public string? address { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? postal_code { get; set; }
    }
}
