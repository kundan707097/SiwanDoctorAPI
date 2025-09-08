using Abp.Domain.Entities.Auditing;
using static SiwanDoctorAPI.DbConnection.ApplicationDbContext;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiwanDoctorAPI.Model.EntityModel.Patient_DetailsInformation
{
    [Table("Patient_Information")]
    public class Patient_Details: FullAuditedEntity
    {
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        public int UserId { get; set; }
        public string? Email { get; set; } // User's email address
        public string? FirstName { get; set; } // First name of the user
        public string? LastName { get; set; } // Last name of the user
        public string? ISDCode { get; set; } // ISD code for phone number
        public string? Phone { get; set; } // Phone number
        public string? Gender { get; set; } // Gender of the user
        public DateTime? DateOfBirth { get; set; } // Date of birth
        [NotMapped]
        public IFormFile Image { get; set; } // Uploaded image file
        public string? patient_Address { get; set; }
        public string? city { get; set; }

        public string? State { get; set; }

        public string? PinCode { get; set; }
        public string? registrationNo { get; set; }
        public bool? IsActive { get; set; }
        public string? ProfileImagePath { get; set; }
    }
}
