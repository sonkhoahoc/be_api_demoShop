using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VHUX.Api.Extensions;
using VHUX.Api.IRepository;
using VHUX.Model;

namespace VHUX.Api.Controllers
{
    [Route("api/vnpay")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly TelegramSend _telegramSend;

        private readonly IPaymentRepository _paymentRepository;
        public PaymentController(IPaymentRepository paymentRepository, TelegramSend telegramSend)
        {
            _paymentRepository = paymentRepository;
            _telegramSend = telegramSend;
        }

        [AllowAnonymous]
        [HttpGet("VnPayIPN")]
        public async Task<ActionResult> VnPayIPN()
        {
            var querystring = HttpContext.Request.QueryString.Value;
            querystring = querystring.Replace("?", "");
            string data = querystring.Replace("&", "/");

            string client_ip = "";
            TelegramModel telegram = new TelegramModel
            {

                title = HttpContext.Connection.RemoteIpAddress?.ToString(),

                note = data,
            };
            _telegramSend.TelegramSendMessage(telegram);
            try
            {

                string respone = await this._paymentRepository.VnPayIPN(querystring, client_ip);
                return Ok(respone);


            }
            catch (Exception)
            {
                return Ok("{\"RspCode\":\"99\",\"Message\":\"Unknow error\"}");
            }
        }
        [HttpGet("payment-return")]
        public async Task<ActionResult> PaymentReturn(string vnp_TmnCode, double vnp_Amount, string vnp_BankCode, string? vnp_BankTranNo, string? vnp_CardType, double? vnp_PayDate, string vnp_OrderInfo, double vnp_TransactionNo,
            string vnp_ResponseCode, string vnp_TransactionStatus, string vnp_TxnRef, string? vnp_SecureHashType, string vnp_SecureHash)
        {
            try
            {

                string accessToken = await HttpContext.GetTokenAsync("access_token");
                string mess = await this._paymentRepository.PaymentReturn(vnp_TmnCode, vnp_Amount, vnp_BankCode, vnp_BankTranNo, vnp_CardType, vnp_PayDate, vnp_OrderInfo, vnp_TransactionNo, vnp_ResponseCode, vnp_TransactionStatus, vnp_TxnRef, vnp_SecureHashType, vnp_SecureHash, accessToken);
                return mess != "0"
                         ? Ok(new ResponseSingleContentModel<string>
                         {
                             StatusCode = 500,
                             Message = "Thêm mới không thành công " + mess,
                             Data = null
                         })
                         : (ActionResult)Ok(new ResponseSingleContentModel<string>
                         {
                             StatusCode = 200,
                             Message = "Thêm mới thành công",
                             Data = mess

                         });


            }
            catch (Exception)
            {
                return Ok(new ResponseSingleContentModel<string>
                {
                    StatusCode = 500,
                    Message = "Có lỗi xảy ra trong quá trình xử lý ",
                    Data = null
                });
            }
        }
    }
}
