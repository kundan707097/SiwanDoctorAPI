using Abp.Domain.Entities.Auditing;
using SiwanDoctorAPI.Model.EntityModel.Doctor_DetailsInformation;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiwanDoctorAPI.Model.EntityModel.DoctorEntity
{
    [Table("Doctor_PrescribeMdicines")]
    public class DoctorPrescribeMdicines : FullAuditedEntity
    {
        [ForeignKey("FK_DoctId")]
        public Doctor_Details doctor_Details { get; set; }
        public int FK_DoctId { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
    }
}
