using System.ComponentModel.DataAnnotations.Schema;
using VHUX.Api.Entity;

namespace VHUX.Api.Entity
{
    [Table("payment_vnpay_hitory")]
    public class Payment_VNPay_Hitory : IAuditableEntity
    {
        public long payment_id { get; set; }
        public string? url { set; get; }
        public string? response { set; get; }
        public string? client_ip { set; get; }
        public byte type { set; get; } // 0 là call/ 1 url, 2 ipn có url respon ipn
    }
}
