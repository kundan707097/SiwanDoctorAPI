using SiwanDoctorAPI.Model.EntityModel.DoctorEntity;

namespace SiwanDoctorAPI.Model.InputDTOModel.TimeSlotInputDTO
{
    public class DoctorTimeSlotResponse
    {
        public int response { get; set; } = 200;
        public List<ListDoctorTimeSlot> data { get; set; }
    }
    public class ListDoctorTimeSlot
    {
        public int id { get; set; }
        public int doct_Id { get; set; } 
        public string? time_start { get; set; }
        public string? time_end { get; set; }
        public int time_duration { get; set; }
        public string? day { get; set; }
        public string? created_at { get; set; }
        public string? updated_at { get; set; }
    }
}
