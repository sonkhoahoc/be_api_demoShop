namespace VHUX.Api.Model.Order
{
    public class PaymentModel
    {
        public double price { get; set; }//  số tiền 
        public long userAdded { get; set; }

        public long customer_id { get; set; }
        public long order_id { get; set; }
        public byte status_id { get; set; } = 0;
        public string vnp_txnref { get; set; }
        public string url { get; set; }
        public byte payment_status_id { get; set; }
        public byte payment_method_id { get; set; }
        public DateTime dateAdded { get; set; }
    }
}
