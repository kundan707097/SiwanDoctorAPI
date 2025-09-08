using SiwanDoctorAPI.Model.EntityModel.AppointmentDetails;

namespace SiwanDoctorAPI.Model.InputDTOModel.AppointmentInputDTO
{
    public class AppointmentResponseByPageStartEnd
    {
        public int response { get; set; }
        public int totalRecord { get; set; }
        public List<AppointmentDate> data { get; set; }
    }
}
