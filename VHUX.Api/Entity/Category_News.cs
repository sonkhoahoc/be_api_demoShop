using System.ComponentModel.DataAnnotations.Schema;

namespace VHUX.Api.Entity
{
    [Table("category_news")]
    public class Category_News:IAuditableEntity
    {
        public string code { get; set; } = "";
        public string name { get; set; } = "";
    }
}
