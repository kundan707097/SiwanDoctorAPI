using System.ComponentModel.DataAnnotations;

namespace SiwanDoctorAPI.Model.InputDTOModel.TimeSlotInputDTO
{
    public class VideoTimeSlotRequest
    {
        [Required]
        public int doct_id { get; set; }

        [Required]
        public string? time_start { get; set; }

        [Required]
        public string? time_end { get; set; }

        [Required]
        public string time_duration { get; set; }

        [Required]
        public string? day { get; set; }
    }
}
