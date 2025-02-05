using Abp.Domain.Entities.Auditing;
using SiwanDoctorAPI.Model.EntityModel.Patient_DetailsInformation;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiwanDoctorAPI.Model.EntityModel.Doctor_DetailsInformation
{
    [Table("Doctor_Reviews")]
    public class DoctorReview : FullAuditedEntity
    {
        [ForeignKey("FK_DoctorId")]
        public Doctor_Details doctor_Details { get; set; }
        public virtual int FK_DoctorId { get; set; }
        public double Points { get; set; }
        public string? Description { get; set; }
        public virtual int FK_UserId { get; set; }
        public int AppointmentId { get; set; }
    }
}
