using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiwanDoctorAPI.Model.EntityModel.DoctorEntity
{
    [Table("Doctor_Department")]
    public class Department : FullAuditedEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }

        public string? DepartmentImage { get; set; }

        public bool Active { get; set; }
    }
}
