using VHUX.Api.Model.Order;

namespace VHUX.Api.IRepository
{
    public interface IPaymentRepository
    {
        Task<string> PaymentCreate(PaymentModel model);
        Task<string> VnPayIPN(string query, string ip);
        Task<string> PaymentReturn(string vnp_TmnCode, double vnp_Amount, string vnp_BankCode, string? vnp_BankTranNo, string? vnp_CardType, double? vnp_PayDate, string vnp_OrderInfo, double vnp_TransactionNo,
         string vnp_ResponseCode, string vnp_TransactionStatus, string vnp_TxnRef, string? vnp_SecureHashType, string vnp_SecureHash, string accessToken);
    }
}
