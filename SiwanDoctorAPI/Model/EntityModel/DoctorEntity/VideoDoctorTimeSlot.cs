using Abp.Domain.Entities.Auditing;
using SiwanDoctorAPI.Model.EntityModel.Doctor_DetailsInformation;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiwanDoctorAPI.Model.EntityModel.DoctorEntity
{
    [Table("VideoDoctor_TimeSlot")]
    public class VideoDoctorTimeSlot: FullAuditedEntity
    {
        public string? TimeStart { get; set; }
        public string? TimeEnd { get; set; }
        public string? TimeDuration { get; set; }
        public string? Day { get; set; }
        [ForeignKey("doct_id")]
        public Doctor_Details Doctor { get; set; }
        public int doct_id { get; set; }
    }
}
