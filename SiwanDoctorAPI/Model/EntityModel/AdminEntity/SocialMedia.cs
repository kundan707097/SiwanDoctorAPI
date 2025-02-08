using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiwanDoctorAPI.Model.EntityModel.AdminEntity
{
    [Table("Social_Media")]
    public class SocialMedia: FullAuditedEntity
    {
        public string? Title { get; set; }
        public string? SocialMediaProfile { get; set; }
        public string? Url { get; set; }
    }
}
