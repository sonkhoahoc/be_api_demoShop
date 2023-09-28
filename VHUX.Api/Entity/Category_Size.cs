using System.ComponentModel.DataAnnotations.Schema;

namespace VHUX.Api.Entity
{
    [Table("category_size")]
    public class Category_Size:IAuditableEntity
    {
        public string code { get; set; }
        public string name { get; set; }
    }
}
