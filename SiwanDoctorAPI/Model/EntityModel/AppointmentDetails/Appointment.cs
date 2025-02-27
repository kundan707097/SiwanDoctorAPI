using SiwanDoctorAPI.Model.EntityModel.Doctor_DetailsInformation;
using SiwanDoctorAPI.Model.EntityModel.Patient_DetailsInformation;

using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;

namespace SiwanDoctorAPI.Model.EntityModel.AppointmentDetails
{
    [Table("Patient_Appointments")]
    public class Appointment : FullAuditedEntity
    {
        

        public int UserFamilyMemberId { get; set; }
        [ForeignKey("UserFamilyMemberId")]
        public UserFamilyMember userFamilyMember { get; set; }

        public string? Status { get; set; }
        public string? Date { get; set; }
        public string? TimeSlots { get; set; }
        [ForeignKey("FK_DoctId")]
        public Doctor_Details doctor_Details { get; set; }
        public int FK_DoctId { get; set; }
        public int DeptId { get; set; }
        public string? Type { get; set; }
        public string? MeetingId { get; set; }
        public string? MeetingLink { get; set; }
        public decimal Fee { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal TotalAmount { get; set; }
        public string? InvoiceDescription { get; set; }
        public string? PaymentMethod { get; set; }
       

        [ForeignKey("UserId")]
        public virtual Patient_Details patient_Details { get; set; }
        public int UserId { get; set; }
        public string? PaymentTransactionId { get; set; }
        public string? PaymentStatus { get; set; }
        public string? CouponId { get; set; }
        public string? CouponTitle { get; set; }
        public string? CouponValue { get; set; }
        public string? CouponOffAmount { get; set; }
        public string? UnitTaxAmount { get; set; }
        public string? Tax { get; set; }
        public decimal UnitTotalAmount { get; set; }
        public string? Source { get; set; }

        public string? currentcancelreqstatus { get; set; }
    }
}
