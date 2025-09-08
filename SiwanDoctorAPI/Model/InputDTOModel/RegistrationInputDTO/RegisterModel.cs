using static SiwanDoctorAPI.DbConnection.ApplicationDbContext;

namespace SiwanDoctorAPI.Model.InputDTOModel.RegistrationInputDTO
{
    public class RegisterModel
    {
        public string f_name { get; set; }
        public string l_name { get; set; }
        public string phone { get; set; }
        public string isd_code { get; set; }
        public string gender { get; set; }
        public string password { get; set; }
        public int UserType { get; set; }
    }

    public enum UserType
    {
        Doctor = 1,
        Patient = 2,
        Staff = 3
    }
}
