using static SiwanDoctorAPI.DbConnection.ApplicationDbContext;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using SiwanDoctorAPI.Model.EntityModel.DoctorEntity;
namespace SiwanDoctorAPI.Model.EntityModel.Doctor_DetailsInformation
{
    [Table("Doctor_Information")]
    public class Doctor_Details: FullAuditedEntity
    {
        
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        public int UserId { get; set; } // Change to int
        public string? Email { get; set; } 
        public string? FirstName { get; set; } 
        public string? LastName { get; set; } 
        
        public int?   ExperienceYears { get; set; } 
        public bool? IsActive { get; set; } 
        public string? ISDCode { get; set; } 
        public string? Phone { get; set; } 
        public string? Gender { get; set; } 
        public DateTime?  DateOfBirth { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; } 
        public string? Specialization { get; set; } 
        public string? doctor_Address { get; set; }
        public bool? AdminVerifyToDoctors { get; set; }
        public string? keywords { get; set; }
        public string? PostalCode { get; set; } 
        public string? ZoomClientId { get; set; } 
        public string? ZoomSecretId { get; set; }
        public string? FacebookLink { get; set; } 
        public string? TwitterLink { get; set; } 
        public string? YouTubeLink { get; set; } 
        public string? InstagramLink { get; set; }
        public decimal? OpdFee { get; set; } 
        public decimal? VideoFee { get; set; } 
        public decimal? EmergencyFee { get; set; }
        public string? registrationNo { get; set; }
        public string? ProfileImagePath { get; set; }
        public string? city { get; set; }
        public string? State { get; set; }
        public string? Description { get; set; }
        public bool StopBooking { get; set; }

        public bool VideoAppointment {  get; set; }
        public bool ClinicAppointment { get; set; }
        public bool EmergencyAppointment { get; set; }
        public int? DepartmentId { get; set; }  // Make nullable

        [ForeignKey("DepartmentId")]
        public virtual Department? department { get; set; }

        public virtual ICollection<DoctorReview> DoctorReview { get; set; }
    }
}
