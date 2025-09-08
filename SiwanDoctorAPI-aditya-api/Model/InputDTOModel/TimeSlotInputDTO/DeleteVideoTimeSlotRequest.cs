using System.ComponentModel.DataAnnotations;

namespace SiwanDoctorAPI.Model.InputDTOModel.TimeSlotInputDTO
{
    public class DeleteVideoTimeSlotRequest
    {
        [Required]
        public string id { get; set; }
    }
}
