using System.ComponentModel.DataAnnotations;

namespace SiwanDoctorAPI.Model.InputDTOModel.TimeSlotInputDTO
{
    public class DeleteTimeSlotRequest
    {
        [Required]
        public string id { get; set; }
    }
}
