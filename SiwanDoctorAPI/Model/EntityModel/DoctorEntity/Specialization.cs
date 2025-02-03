using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiwanDoctorAPI.Model.EntityModel.Department
{
    [Table("Doctor_Specialization")]
    public class Specialization : FullAuditedEntity
    {
        public string? Title { get; set; }
    }
}
