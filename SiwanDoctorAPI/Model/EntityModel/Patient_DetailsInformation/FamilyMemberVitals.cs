using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiwanDoctorAPI.Model.EntityModel.Patient_DetailsInformation
{
    [Table("Family_MemberVitals")]
    public class FamilyMemberVitals : FullAuditedEntity
    {
        public int FK_patient_Details { get; set; }
        [ForeignKey("FK_userFamilyMember")]
        public UserFamilyMember userFamilyMember { get; set; }
        public int FK_userFamilyMember { get; set; }
        public int BpSystolic { get; set; }
        public int BpDiastolic { get; set; }
        public int Weight { get; set; }
        public int Spo2 { get; set; }
        public int Temperature { get; set; }
        public int SugarRandom { get; set; }
        public int SugarFasting { get; set; }
        public string? Type { get; set; }
        public string? Date { get; set; }
        public string? Time { get; set; }
    }
}
