using System.ComponentModel.DataAnnotations.Schema;

namespace VHUX.Api.Entity
{
    [Table("category_product")]
    public class Category_Product:IAuditableEntity
    {
        public string code { get; set; } = "";
        public string name { get; set; } = "";
      
    }
}
