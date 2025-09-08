namespace SiwanDoctorAPI.Model.InputDTOModel.PatientInputDTO
{
    public class AddFamilyMemberRequest
    {
        public int patienttableId { get; set; }
        public string? f_name { get; set; } // First Name
        public string? l_name { get; set; } // Last Name
        public string? phone { get; set; } // Phone Number
        public string? isd_code { get; set; } // ISD Code
        public int user_id { get; set; } // User ID
        public string? gender { get; set; } // Gender
        public DateTime? dob { get; set; } // Date of Birth
    }


    public class AddFamilyMemberRequestbyDoctor
    {
        public int patienttableId { get; set; }
        public string? f_name { get; set; } // First Name
        public string? l_name { get; set; } // Last Name
        public string? phone { get; set; } // Phone Number
        public string? isd_code { get; set; } // ISD Code
        public int user_id { get; set; } // User ID
        public string? gender { get; set; } // Gender
        public DateTime? dob { get; set; } // Date of Birth
    }
}
