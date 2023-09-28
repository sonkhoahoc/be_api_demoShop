using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VHUX.Api.Entity;

namespace VHUX.Api.Entity
{
    [Table("payment_vnpay_order")]
    public class Payment_VNPay_Order: IAuditableEntity
    {
  
        public double price { get; set; }//  số tiền 
        [StringLength(250)]
        public string? status { get; set; }// trạng thái
        public byte payment_method_id { set; get; } = 0;
        [StringLength(250)]

        public string vnp_orderinfo { get; set; }
        [StringLength(100)]
        public string vnp_txnref { get; set; }
        public string? url { get; set; }
        public byte payment_status_id { get; set; }
        public long customer_id { get; set; }
        public long order_id { get; set; }
        public byte status_id { get; set; } = 0;
    }
}
