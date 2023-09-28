
using VHUX.Api.Entity;

namespace VHUX.API.Model.Customer
{
    public class CustomerModel : IAuditableEntity
    {
        public long id { set; get; }
        public string name { set; get; } = string.Empty;
        public string? address { set; get; }
        public string? customer_affliate { set; get; }
        public string phone { set; get; } = string.Empty;
        public DateTime? birthday { set; get; }
        public string taxcode { set; get; } = string.Empty;
        public string username { set; get; } = string.Empty;
        public int point { set; get; }
        public string affliate { set; get; }

    }
    public class CustomerAddModel
    {
        public string phone { get; set; }
        public string name { get; set; } = "";
        public string password { get; set; }
        public string? customer_affliate { set; get; }

    }
    public class CustomerSignatureModel
    {
        public long id { set; get; }
        public string file { set; get; }
    }
    public class CustomerLoginOTPModel
    {
        public string full_name { get; set; } = string.Empty;
        public string username { get; set; } = string.Empty;

        public long customer_id { set; get; }
        public int checkLogin { set; get; } //1 otp đã hết hạn,2 OTP không chính xác, 3 tài khoản chưa tồn tại, 4 thanh cong
    }
    public class CustomerClaimModel
    {
        public long id { get; set; }
        public string username { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string full_name { get; set; } = string.Empty;
        public byte type { set; get; }

    }
}
