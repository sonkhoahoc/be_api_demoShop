using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VHUX.Api.Entity;

namespace VHUX.Api.Entity
{
    [Table("vnpay_ipn")]
    public class Vnpay_IPN : IAuditableEntity
    {
        [Column("vnp_tmn_code")]
        public string vnp_TmnCode { get; set; }//Mã website của merchant trên hệ thống của VNPAY. Ví dụ: 2QXUI4J4
        [Column("vnp_amount")]
        public double vnp_Amount { get; set; }//Số tiền thanh toán. VNPAY phản hồi số tiền nhân thêm 100 lần.
        [Column("vnp_bank_code")]
        public string vnp_BankCode { get; set; }//Mã Ngân hàng thanh toán. Ví dụ: NCB
        [Column("vnp_vank_tranno")]
        public string? vnp_BankTranNo { get; set; }//Mã giao dịch tại Ngân hàng. Ví dụ: NCB20170829152730
        [StringLength(25)]
        [Column("vnp_cardtype")]
        public string? vnp_CardType { get; set; }//Loại tài khoản/thẻ khách hàng sử dụng:ATM,QRCODE
        [Column("vnp_paydate")]
        public double? vnp_PayDate { get; set; }//Thời gian thanh toán. Định dạng: yyyyMMddHHmmss
        [Column("vnp_order_info")]
        public string vnp_OrderInfo { get; set; }//Thông tin mô tả nội dung thanh toán (Tiếng Việt, không dấu). Ví dụ: **Nap tien cho thue bao 0123456789. So tien 100,000 VND**

        [Column("vnp_transaction_no")]
        public double vnp_TransactionNo { get; set; }//Mã giao dịch ghi nhận tại hệ thống VNPAY. Ví dụ: 20170829153052
        [Column("vnp_responsecode")]
        public string vnp_ResponseCode { get; set; }//Mã phản hồi kết quả thanh toán. Quy định mã trả lời 00 ứng với kết quả Thành công cho tất cả các AP
        [Column("vnp_transaction_status")]
        public string vnp_TransactionStatus { get; set; }//Mã phản hồi kết quả thanh toán.Tình trạng của giao dịch tại Cổng thanh toán VNPAY.
                                                         //-00: Giao dịch thanh toán được thực hiện thành công tại VNPAY
                                                         //-Khác 00: Giao dịch không thành công tại VNPAY
        [StringLength(10)]
        [Column("vnp_txnref")]
        public string vnp_TxnRef { get; set; }
        [StringLength(300)]
        [Column("vnp_secure_hashtype")]
        public string vnp_SecureHashType { get; set; }
        [StringLength(300)]
        [Column("vnp_securehash")]
        public string vnp_SecureHash { get; set; }

        public byte type { set; get; } // 1 là fe return 2 là VNp return
    }
}
