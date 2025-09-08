using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SiwanDoctorAPI.Model
{
    [Table("configurations", Schema = "dbo")]
    public class configurations
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("id_name")]
        public string IdName { get; set; }

        [Column("group_name")]
        public string GroupName { get; set; }

        [Column("preferences")]
        public int Preferences { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("value")]
        public string Value { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
    public class configurationsDto
    {
        [Key]
        
        public int id { get; set; }

    
        public string? id_name { get; set; }

     
        public string? group_name { get; set; }

      
        public int? preferences { get; set; }

        public string? title { get; set; }


        public string? value { get; set; }

   
        public string? description { get; set; }

   
        public DateTime? created_at { get; set; }


        public DateTime? updated_at { get; set; }
    }
   


    // Class to represent the JSON response structure
    public class ConfigurationResponse
    {
        public int Response { get; set; }
        public List<configurations> Data { get; set; }
    }


}
