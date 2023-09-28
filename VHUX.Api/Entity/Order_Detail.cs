using System.ComponentModel.DataAnnotations.Schema;

namespace VHUX.Api.Entity
{
    [Table("order_detail")]
    public class Order_Detail:IAuditableEntity
    {
        public long order_id { get; set; }
        public long product_id { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
    }
}
