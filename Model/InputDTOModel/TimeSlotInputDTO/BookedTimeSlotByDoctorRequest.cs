using SiwanDoctorAPI.Model.EntityModel.AppointmentDetails;

namespace SiwanDoctorAPI.Model.InputDTOModel.TimeSlotInputDTO
{
    public class BookedTimeSlotByDoctorRequest
    {
        public int doct_id { get; set; }
        public string? date { get; set; }
        public string? type { get; set; }
    }


    public class BookedTimeSlotByDoctorResponse
    {
        public int response { get; set; }
        public List<BookedTimeSlotResponse> data { get; set; }
    }
    public class BookedTimeSlotResponse
    {
        public string time_slots { get; set; }
        public string date { get; set; }
        public string type { get; set; }
        public int appointment_id { get; set; }
    }
}
