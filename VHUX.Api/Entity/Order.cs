using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VHUX.Api.Entity
{
    [Table("order")]
    public class Order : IAuditableEntity
    {
        public long customer_id { set; get; }
        [Required]
        public string customer_name { get; set; }  //tên người nhận
        public string customer_phone { get; set; } = "";  //email người nhận
        public long customer_provice_id { get; set; }  // id thành phố
        public byte payment_status_id { get; set; }  //trạng thái thanh toán 
        [StringLength(250)]
        public string? customer_provice_name { get; set; }  // id thành phố
        public long customer_dictrics_id { get; set; }//id quận huyện
        [StringLength(250)]
        public string? customer_dictrics_name { get; set; }//id quận huyện
        public long customer_wards_id { get; set; } // id phường xã
        [StringLength(250)]
        public string? customer_wards_name { get; set; } // tên phường xã
        [StringLength(250)]
        public string customer_address { get; set; }
        public double total_price { get; set; }
        public double total_product { get; set; }
        public double shiping_cost { get; set; }
        [StringLength(250)]
        public string customer_note { get; set; }
        public byte status { get; set; }
        [NotMapped]
        public List<Order_Detail> orderDetail { get; set; }

    }
}
