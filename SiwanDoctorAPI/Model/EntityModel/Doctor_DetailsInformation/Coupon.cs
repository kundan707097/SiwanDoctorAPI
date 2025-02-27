using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiwanDoctorAPI.Model.EntityModel.Doctor_DetailsInformation
{
    [Table("Doctor_Coupon")]
    public class Coupon: FullAuditedEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
    }
}
