using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiwanDoctorAPI.Model.EntityModel.SettingEntity
{
    [Table("Web_Page")]
    public class WebPage : FullAuditedEntity
    {
        public int PageId { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
    }
}
