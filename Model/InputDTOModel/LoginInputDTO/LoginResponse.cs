namespace SiwanDoctorAPI.Model.InputDTOModel.LoginInputDTO
{
    public class LoginResponse
    {
        public int response { get; set; }
        public bool status { get; set; }
        public string message { get; set; }
        public UserData data { get; set; }
        public string token { get; set; }
    }

    public class UserData
    {
        public int id { get; set; }
        public decimal wallet_amount { get; set; }
        public string f_name { get; set; }
        public string l_name { get; set; }
        public string phone { get; set; }
        public string isd_code { get; set; }
        public string gender { get; set; }
        public string dob { get; set; }
        public string email { get; set; }
        public string image { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int postal_code { get; set; }
        public string isd_code_sec { get; set; }
        public string phone_sec { get; set; }
        public string email_verified_at { get; set; }
        public string fcm { get; set; }
        public string web_fcm { get; set; }
        public string notification_seen_at { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public int is_deleted { get; set; }
        public string deleted_at { get; set; }
        public List<UserRole> role { get; set; }
    }

    public class UserRole
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int role_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string name { get; set; }
    }

}
