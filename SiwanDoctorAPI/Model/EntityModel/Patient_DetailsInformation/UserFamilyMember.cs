using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiwanDoctorAPI.Model.EntityModel.Patient_DetailsInformation
{
    [Table("User_FamilyMember")]
    public class UserFamilyMember : FullAuditedEntity
    {
        public string? F_Name { get; set; }  // First Name
        public string? L_Name { get; set; }  // Last Name
        public string? Phone { get; set; }   // Phone Number
        public string? Isd_Code { get; set; }  // ISD Code


        [ForeignKey("User_Id")]
        public Patient_Details PatientDetails { get; set; }  // Navigation property

        public int User_Id { get; set; } // Foreign key for Patient_Details
        public string? Gender { get; set; }  // Gender
        public DateTime Dob { get; set; }  // Date of Birth (Nullable)

    }
}
