using Abp.Domain.Entities.Auditing;
using SiwanDoctorAPI.Model.EntityModel.AppointmentDetails;
using SiwanDoctorAPI.Model.EntityModel.Doctor_DetailsInformation;
using SiwanDoctorAPI.Model.EntityModel.Patient_DetailsInformation;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiwanDoctorAPI.Model.EntityModel.Payment_Entity
{
    [Table("Payment_Details")]
    public class PaymentDetails : FullAuditedEntity
    {
        
        [ForeignKey("AppointmentId")]
        public virtual Appointment Appointment { get; set; }
        public int AppointmentId { get; set; }
        public string RazorpayOrderId { get; set; }
        public string? RazorpayPaymentId { get; set; }
        public string? RazorpaySignature { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;
    }
}
