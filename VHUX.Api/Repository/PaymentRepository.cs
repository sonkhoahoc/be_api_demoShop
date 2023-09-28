using Microsoft.Extensions.Options;
using VHUX.Api.Entity;
using VHUX.Api.Extensions;
using VHUX.Api.IRepository;
using VHUX.Api.Model.Order;
using VHUX.Extensions;
using VHUX.Model;

namespace VHUX.Api.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationContext _context;
        private readonly ConnectServicesSetting _appsetingUrl;
        private readonly TelegramSend _telegram;
        public PaymentRepository(IOptions<ConnectServicesSetting> settings, ApplicationContext context, TelegramSend telegram)
        {
            _context = context;
            _appsetingUrl = settings.Value;
            _telegram = telegram;

        }

        public async Task<string> PaymentCreate(PaymentModel model)
        {
            return await Task.Run(() =>
            {
                string mess = "0";
                try
                {


                    Payment_VNPay_Order payment = new Payment_VNPay_Order
                    {
                        customer_id = model.customer_id,
                        status_id = (byte)Common.PaymentStatus.CHUATHANHTOAN,
                        status = "0",
                        dateAdded = model.dateAdded,
                        price = model.price,
                        order_id = model.order_id,
                        vnp_orderinfo = "",
                        url = model.url,
                        vnp_txnref = model.vnp_txnref,

                    };

                    _context.Payment_VNPay_Order.Add(payment);
                    _context.SaveChanges();


                    Payment_VNPay_Hitory hitory = new Payment_VNPay_Hitory
                    {
                        client_ip = "",
                        payment_id = payment.id,
                        response = "",
                        type = 0,
                        url = model.url,
                        dateAdded = DateTime.Now,

                    };
                    _context.Payment_VNPay_Hitory.Add(hitory);

                    _context.SaveChanges();
                }
                catch (Exception)
                {

                    mess = "có lỗi trong quá trình xử lý";
                }


                return mess;
            });

        }

        public async Task<string> VnPayIPN(string query, string ip)
        {
            string vnp_HashSecret = _appsetingUrl.vnp_HashSecret;

            string mess = "{\"RspCode\":\"99\",\"Message\":\"Unknow error\"}";
            try
            {
                Payment_VNPay_Hitory hitory = new Payment_VNPay_Hitory
                {
                    client_ip = ip,
                    payment_id = 0,
                    response = "",
                    type = 2,
                    url = query,
                    dateAdded = DateTime.Now,


                };

                string[] listQuery = query.Split("&");
                VnPayLibrary vnpaycheck = new VnPayLibrary();
                string vnp_TxnRef = "";
                string vnp_Amount = "0";
                string vnp_ResponseCode = "";
                string vnp_TransactionStatus = "";
                foreach (var item in listQuery)
                {
                    string[] value = item.Split("=");
                    if (value[0] == "vnp_TxnRef")
                        vnp_TxnRef = value[1];
                    if (value[0] == "vnp_Amount")
                        vnp_Amount = value[1];
                    if (value[0] == "vnp_ResponseCode")
                        vnp_ResponseCode = value[1];
                    if (value[0] == "vnp_TransactionStatus")
                        vnp_TransactionStatus = value[1];
                }

                bool checkSignature = vnpaycheck.ValidateSignature(query, vnp_HashSecret);
                if (checkSignature)
                {
                    //Cap nhat ket qua GD
                    //Yeu cau: Truy van vao CSDL cua  Merchant => lay ra duoc OrderInfo
                    //Giả sử OrderInfo lấy ra được như giả lập bên dưới
                    Payment_VNPay_Order order = _context.Payment_VNPay_Order.FirstOrDefault(r => r.vnp_txnref == vnp_TxnRef);//get from DB

                    if (order != null)
                    {
                        hitory.payment_id = order.id;
                        double price = double.Parse(vnp_Amount) / 100;
                        if (order.price == price)
                        {
                            if (order.status_id == 0)
                            {
                                if (order.status == "0")
                                {
                                    string mess1 = "0";
                                    if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                                    {
                                        //Thanh toan thanh cong
                                        //log.InfoFormat("Thanh toan thanh cong, OrderId={0}, VNPAY TranId={1}", orderId,
                                        //    vnpayTranId);
                                        order.status = "1";
                                        mess1 = await OrderApprove(vnp_TxnRef, (byte)Common.PaymentStatus.DAXACNHAN);
                                    }
                                    else
                                    {

                                        order.status = "2";
                                        mess1 = await OrderApprove(vnp_TxnRef, (byte)Common.PaymentStatus.THANHTOANLOI);
                                    }
                                    if (mess1 != "0")
                                    {
                                        return mess = "{\"RspCode\":\"01\",\"Message\":\"Order not found\"}";
                                    }

                                    //Update Database
                                    _context.Payment_VNPay_Order.Update(order);
                                    mess = "{\"RspCode\":\"00\",\"Message\":\"Confirm Success\"}";
                                }
                                else
                                {
                                    mess = "{\"RspCode\":\"02\",\"Message\":\"Order already confirmed\"}";
                                }
                            }
                            else
                            {
                                mess = "{\"RspCode\":\"02\",\"Message\":\"Order already confirmed\"}";

                            }
                        }
                        else
                        {
                            mess = "{\"RspCode\":\"04\",\"Message\":\"invalid amount\"}";
                        }



                    }
                    else
                    {
                        mess = "{\"RspCode\":\"01\",\"Message\":\"Order not found\"}";
                    }
                }
                else
                {
                    mess = "{\"RspCode\":\"97\",\"Message\":\"Invalid signature\"}";
                }
                hitory.response = mess;
                _context.Payment_VNPay_Hitory.Add(hitory);

                _context.SaveChanges();
            }
            catch (Exception)
            {
                mess = "{\"RspCode\":\"99\",\"Message\":\"Unknow error\"}";

            }


            return mess;
        }

        private async Task<string> OrderApprove(string vnp_TxnRef, byte status)
        {
            string mess = "0";
            Payment_VNPay_Order payment = _context.Payment_VNPay_Order.Where(r => r.vnp_txnref == vnp_TxnRef && r.status_id == 0).FirstOrDefault();

            payment.status_id = 1;

            var order = _context.Order.Where(r => r.id == payment.order_id).FirstOrDefault();
            if (status == (byte)Common.PaymentStatus.DAXACNHAN)
            {
                int quantity = 0;

                order.payment_status_id = (byte)Common.PaymentStatus.DAXACNHAN;
                var listOrderDetail = _context.Order_Detail.Where(r => r.order_id == payment.order_id).ToList();
                foreach (var item in listOrderDetail)
                {
                    quantity = quantity + item.quantity;

                }
                UpdatePointCustomer(order.customer_id, quantity);
            }
            else
            {
                order.payment_status_id = (byte)Common.PaymentStatus.THANHTOANLOI;

            }
            _context.Order.UpdateRange(order);
            _context.Payment_VNPay_Order.Update(payment);

            TelegramModel telegrampush = new TelegramModel
            {
                //title = "Đơn hàng",
                note = "vnp_TxnRef+" + vnp_TxnRef + "/vnp_TxnRef+" + status + "/mess_reponse" + mess,

            };
            _telegram.TelegramSendMessage(telegrampush);
            _context.SaveChanges();
            return mess;
        }
        private void UpdatePointCustomer(long customer_id, int quantity)
        {
            var customer = _context.Customer.Where(r => r.id == customer_id).FirstOrDefault();
            customer.point = customer.point + quantity * 2;
            _context.Customer.Update(customer);
            var customer_affliate = _context.Customer.Where(r => r.customer_affliate == customer.customer_affliate).FirstOrDefault();
            if (customer_affliate != null)
            {
                customer_affliate.point = customer_affliate.point + quantity * 2;
                _context.Customer.Update(customer_affliate);
                _context.SaveChanges();
            }
        }
        public async Task<string> PaymentReturn(string vnp_TmnCode, double vnp_Amount, string vnp_BankCode, string? vnp_BankTranNo, string? vnp_CardType, double? vnp_PayDate, string vnp_OrderInfo, double vnp_TransactionNo,
          string vnp_ResponseCode, string vnp_TransactionStatus, string vnp_TxnRef, string? vnp_SecureHashType, string vnp_SecureHash, string accessToken)
        {
            string mess = "0";
            string vnp_HashSecret = _appsetingUrl.vnp_HashSecret;
            VnPayLibrary vnpaycheck = new VnPayLibrary();

            Vnpay_IPN vnpay = new Vnpay_IPN
            {
                vnp_SecureHash = vnp_SecureHash,
                vnp_SecureHashType = vnp_SecureHashType ?? "",
                dateAdded = DateTime.Now,
                dateUpdated = DateTime.Now,
                vnp_Amount = vnp_Amount,
                vnp_BankCode = vnp_BankCode,
                vnp_CardType = vnp_CardType,
                vnp_OrderInfo = vnp_OrderInfo,
                vnp_PayDate = vnp_PayDate,
                vnp_TransactionStatus = vnp_TransactionStatus,
                vnp_BankTranNo = vnp_BankTranNo,
                vnp_ResponseCode = vnp_ResponseCode,
                vnp_TransactionNo = vnp_TransactionNo,
                vnp_TmnCode = vnp_TmnCode,
                vnp_TxnRef = vnp_TxnRef,
                type = 2


            }; _context.Vnpay_IPN.Add(vnpay);
            _context.SaveChanges();

            //Payment_VNPay_Order payment = _context.Payment_VNPay_Order.Where(r => r.vnp_txnref == vnp_TxnRef).FirstOrDefault();

            //}
            //else
            //{
            //    mess = "Mã xác nhận của bạn không hợp lệ";
            //}
            return mess;
        }
    }
}
