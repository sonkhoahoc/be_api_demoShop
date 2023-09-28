using System.ComponentModel.DataAnnotations.Schema;

namespace VHUX.Api.Entity
{
    [Table("product_file")]
    public class Product_File:IAuditableEntity
    {
        public long product_id { get; set; }
        public string file { get; set; }
    }
}
