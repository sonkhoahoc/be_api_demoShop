
using System.ComponentModel.DataAnnotations.Schema;

namespace VHUX.Api.Entity
{
   
    [Table("customer_cart")]
    public class Customer_Cart : IAuditableEntity
    {
        public long customer_id { get; set; }
        public long product_id { get; set; }
        public int quantity { get; set; }
        public string product_size { get; set; }
    }
}
