using System.ComponentModel.DataAnnotations.Schema;

namespace VHUX.Api.Entity
{
    [Table("order_payment")]
    public class Orders_Payment : IAuditableEntity
    {
        public long order_id { get; set; }
        public double order_price { get; set; }
        public long payment_order_id { get; set; }
        public long customer_id { get; set; }
        public byte payment_status_id { get; set; }
    }
}
