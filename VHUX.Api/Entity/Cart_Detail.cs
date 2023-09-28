using System.ComponentModel.DataAnnotations.Schema;

namespace VHUX.Api.Entity
{
    [Table("cart_detail")]
    public class Cart_Detail:IAuditableEntity
    {
        public long cart_id { get; set; }
        public long product_id { get; set; }
        public int quantity { get; set; }
    }
}
