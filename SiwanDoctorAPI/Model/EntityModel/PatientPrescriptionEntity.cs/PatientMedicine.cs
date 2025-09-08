using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiwanDoctorAPI.Model.EntityModel.PatientPrescriptionEntity.cs
{
    [Table("Patient_Medicine")]
    public class PatientMedicine : FullAuditedEntity
    {
        public string? MedicineName { get; set; }
        public string? Dosage { get; set; }
        public string? Duration { get; set; }
        public string? Time { get; set; }
        public string? DoseInterval { get; set; }
        public string? Notes { get; set; }

        [ForeignKey("PatientPrescriptionId")]
        public PatientPrescription Prescription { get; set; }
    }
}
