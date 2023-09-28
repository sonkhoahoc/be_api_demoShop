namespace VHUX.Api.Model.Customer
{
    public class CustomerCartModel
    {
        public long customer_id { get; set; }
        public long product_id { get; set; }
        public int quantity { get; set; }
        public string product_size { get; set; }
        public string product_name { get; set; }
        public string product_avatar { get; set; }
        
    }
}
