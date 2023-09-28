using System.ComponentModel.DataAnnotations.Schema;

namespace VHUX.Api.Entity
{
    [Table("cart")]
    public class Cart : IAuditableEntity
    {
        public long customer_id { get; set; }
        [NotMapped]
        public List<Cart_Detail> cart_Details { get; set; }
    }
}
