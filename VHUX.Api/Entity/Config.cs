using System.ComponentModel.DataAnnotations.Schema;

namespace VHUX.Api.Entity
{
    [Table("config")]
    public class Config : IAuditableEntity
    {
        public string company_name { get; set; } = "";
        public string address { get; set; } = "";
        public string hotline { get; set; } = "";
        public string email { get; set; } = "";
        public string map_iframe { get; set; } = "";
    }
}
