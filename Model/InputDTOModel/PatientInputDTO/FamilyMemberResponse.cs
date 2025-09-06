namespace SiwanDoctorAPI.Model.InputDTOModel.PatientInputDTO
{
    public class FamilyMemberResponse
    {
        public int response { get; set; } // HTTP Response Code (200, 201, 400, etc.)
        public bool status { get; set; } // true/false
        public string? message { get; set; }
    }
}
