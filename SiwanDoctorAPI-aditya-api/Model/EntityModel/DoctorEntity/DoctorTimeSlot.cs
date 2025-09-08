using Abp.Domain.Entities.Auditing;
using SiwanDoctorAPI.Model.EntityModel.Doctor_DetailsInformation;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace SiwanDoctorAPI.Model.EntityModel.DoctorEntity
{
    [Table("Doctor_TimeSlot")]
    public class DoctorTimeSlot : FullAuditedEntity
    {
        public string? Name { get; set; }
        public string? Specialization { get; set; }

        public string? TimeStart { get; set; }
        public string? TimeEnd { get; set; }
        public string? TimeDuration { get; set; }
        public string? Day { get; set; }
        [ForeignKey("doct_id")]
        public Doctor_Details Doctor { get; set; }
        public int doct_id { get; set; }
    }

}
