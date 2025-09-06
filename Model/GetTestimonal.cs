using System.ComponentModel.DataAnnotations.Schema;

namespace SiwanDoctorAPI.Model
{
    public class GetTestimonal
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        [Column("sub_title")]
        public string? SubTitle { get; set; }
        public string? Image { get; set; }
        public int? Rating { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
