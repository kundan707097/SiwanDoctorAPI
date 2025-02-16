using Abp.Domain.Entities.Auditing;
using SiwanDoctorAPI.Model.EntityModel.AppointmentDetails;
using SiwanDoctorAPI.Model.EntityModel.Doctor_DetailsInformation;
using SiwanDoctorAPI.Model.EntityModel.Patient_DetailsInformation;
using SiwanDoctorAPI.Model.InputDTOModel.PrescribedMedicine;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiwanDoctorAPI.Model.EntityModel.PatientPrescriptionEntity.cs
{
    [Table("Patient_Prescription")]
    public class PatientPrescription : FullAuditedEntity
    {
        public int AppointmentId { get; set; }
        [ForeignKey("AppointmentId")]
        public Appointment appointment { get; set; }
        public int UserFamilyMemberId { get; set; }
        [ForeignKey("UserFamilyMemberId")]
        public UserFamilyMember userFamilyMember { get; set; }
        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor_Details doctor_Details { get; set; }
        public string? Test { get; set; }
        public string? Advice { get; set; }
        public string? ProblemDesc { get; set; }
        public string? FoodAllergies { get; set; }
        public string? TendencyBleed { get; set; }
        public string? HeartDisease { get; set; }
        public string? BloodPressure { get; set; }
        public string? Diabetic { get; set; }
        public string? Surgery { get; set; }
        public string? Accident { get; set; }
        public string? Others { get; set; }
        public string? MedicalHistory { get; set; }
        public string? CurrentMedication { get; set; }
        public string? FemalePregnancy { get; set; }
        public string? BreastFeeding { get; set; }
        public string? PulseRate { get; set; }
        public string? Temperature { get; set; }
        public int NextVisit { get; set; }
        public ICollection<PatientMedicine> Medicines { get; set; }

    }
}
