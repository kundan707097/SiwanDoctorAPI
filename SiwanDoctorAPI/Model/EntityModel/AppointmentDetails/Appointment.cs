﻿using SiwanDoctorAPI.Model.EntityModel.Doctor_DetailsInformation;
using SiwanDoctorAPI.Model.EntityModel.Patient_DetailsInformation;
using SiwanDoctorAPI.Model.EntityModel.Department;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;

namespace SiwanDoctorAPI.Model.EntityModel.AppointmentDetails
{
    [Table("Patient_Appointments")]
    public class Appointment : FullAuditedEntity
    {
        [ForeignKey("FK_PatientId")]
        public virtual Patient_Details patient_Details { get; set; }
        public virtual int FK_PatientId { get; set; }
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
    }
}
