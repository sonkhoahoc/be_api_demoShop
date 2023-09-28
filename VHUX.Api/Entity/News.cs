using System.ComponentModel.DataAnnotations.Schema;

namespace VHUX.Api.Entity
{
    [Table("news")]
    public class News:IAuditableEntity
    {
        public long category_id { get; set; }
        public string title { get; set; } = ""; 
        public string description { get; set; } = "";
        public string content { get; set; } = "";
        public string note { set; get; } = "";
        public string? meta_title { get; set; }
        public string? meta_description { get; set; }
        public string? meta_keywords { get; set; }

        public string? files { get; set; }

    }
}
