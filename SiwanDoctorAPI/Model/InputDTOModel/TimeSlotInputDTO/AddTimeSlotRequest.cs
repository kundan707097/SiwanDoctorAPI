using System.ComponentModel.DataAnnotations;

namespace SiwanDoctorAPI.Model.InputDTOModel.DoctorInputDTO
{
    public class AddTimeSlotRequest
    {
        [Required]
        public int doct_id { get; set; } // Doctor ID (Foreign Key)

        [Required]
        public string? time_start { get; set; } // Start Time (HH:mm format)

        [Required]
        public string? time_end { get; set; } // End Time (HH:mm format)

        [Required]
        public int time_duration { get; set; } // Duration in minutes

        [Required]
        public string? day { get; set; }
    }
}
