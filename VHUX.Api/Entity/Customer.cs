using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VHUX.Api.Entity
{
    [Table("customer")]
    public class Customer:IAuditableEntity
    {
        public string? phone { set; get; }
        public string? email { set; get; }
        public string name { set; get; }
        public string? address { set; get; }
        public DateTime? birthday { set; get; }
        public string? password { set; get; }
        public string? signature { set; get; }
        [StringLength(50)]
        public string? customer_affliate { set; get; }
        public int point { set; get; }
        public string affliate { set; get; }

    }
}
