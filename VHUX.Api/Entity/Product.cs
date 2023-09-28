using System.ComponentModel.DataAnnotations.Schema;

namespace VHUX.Api.Entity
{
    [Table("product")]
    public class Product : IAuditableEntity
    {
        public string name { set; get; } = "";
        public string code { set; get; } = "";
        public long category_id { set; get; }
        public long category_size_id { set; get; }
        public double price { set; get; }
        public string? description { set; get; }
        public double price_sale { set; get; }
        public int quantity_sold { set; get; } = 0;// số lượng đã bán
        public double ratio { set; get; } = 0;// đánh giá
        public int amount { set; get; } = 0;// tổng số đánh giá
        public string? note { set; get; }
        public string? meta_title { get; set; }
        public string? meta_description { get; set; }
        public string? meta_keywords { get; set; }
        public bool is_active { set; get;}
        [NotMapped]
        public List<Product_File> files { set; get; }
    }
}
