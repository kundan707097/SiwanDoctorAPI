namespace SiwanDoctorAPI.Model.InputDTOModel.AppointmentInputDTO
{
    public class UpdateAppointmentStatus
    {
        public int id { get; set; }
        public string? status { get; set; }
    }

    public class UpdateStatusResponse
    {
        public int response { get; set; }
        public bool status { get; set; }
        public string? message { get; set; }
    }
}
