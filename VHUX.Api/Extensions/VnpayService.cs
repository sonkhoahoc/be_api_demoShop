using Microsoft.Extensions.Options;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using VHUX.Api.Entity;
using VHUX.Api.Model.Order;
using VHUX.Model;

namespace VHUX.Api.Extensions
{
    public class VnpayService
    {
        private readonly ConnectServicesSetting _appsetingUrl;
        public VnpayService(IOptions<ConnectServicesSetting> settings)
        {
            _appsetingUrl = settings.Value;

        }
        public async Task<string> OrderVnPay(PaymentModel model)
        {

            string Vnpay_Url = _appsetingUrl.Vnpay_Url;
            string vnp_Version = _appsetingUrl.vnp_Version;
            string vnp_TmnCode = _appsetingUrl.vnp_TmnCode;
            string vnp_HashSecret = _appsetingUrl.vnp_HashSecret;
            string vnp_Locale = _appsetingUrl.vnp_Locale;
            string vnp_OrderType = _appsetingUrl.vnp_OrderType;
            string vnp_ReturnUrl = _appsetingUrl.vnp_ReturnUrl;


            //Get payment input

            //Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", vnp_Version);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (model.price * 100).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000

            vnpay.AddRequestData("vnp_CreateDate", model.dateAdded.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());

            vnpay.AddRequestData("vnp_Locale", vnp_Locale);

            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + model.vnp_txnref);
            vnpay.AddRequestData("vnp_OrderType", vnp_OrderType); //default value: other
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_ReturnUrl);
            vnpay.AddRequestData("vnp_TxnRef", model.vnp_txnref); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày
                                                                  //Add Params of 2.1.0 Version

            string paymentUrl = vnpay.CreateRequestUrl(Vnpay_Url, vnp_HashSecret);
            return paymentUrl;

        }
    }
    public class VnPayLibrary
    {

        private SortedList<String, String> _requestData = new SortedList<String, String>(new VnPayCompare());
        private SortedList<String, String> _responseData = new SortedList<String, String>(new VnPayCompare());
        public void AddRequestData(string key, string value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                _requestData.Add(key, value);
            }
        }
        public void AddResponseData(string key, string value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                _responseData.Add(key, value);
            }
        }

        public bool ValidateSignature(string query, string vnp_SecureHash)
        {
            string secretKey = "";
            string[] listQuery = query.Split("&");
            StringBuilder data = new StringBuilder(); string rspRaw = "";
            foreach (string item in listQuery)
            {
                string[] value = item.Split("=");
                string propertyName = value[0];

                if (propertyName != "vnp_SecureHashType" && propertyName != "vnp_SecureHash" )
                {
                    try
                    {
                        string? propertyValue = value[1].ToString();
                        if (propertyName == "vnp_OrderInfo")
                        {
                            propertyValue = propertyValue.Replace("%20", " ");
                          //  propertyValue = propertyValue.Replace("%3A", " ");

                           
                        }    
                        if (propertyValue != null && propertyName.StartsWith("vnp_"))
                            rspRaw = rspRaw + propertyName + "=" + propertyValue + "&";
                            //data.Append(WebUtility.UrlEncode(propertyName) + "=" + WebUtility.UrlEncode(propertyValue) + "&");
                    }
                    catch (Exception)
                    {


                    }

                }
                else if (propertyName == "vnp_SecureHash")
                {
                    secretKey = value[1].ToString();
                }


            }
            if (rspRaw.Length > 0)
            {
                rspRaw=rspRaw.Remove(rspRaw.Length - 1, 1);
            }

            //string rspRaw = data.ToString();

            string myChecksum = Utils.HmacSHA512(vnp_SecureHash, rspRaw);
            return myChecksum.Equals(secretKey, StringComparison.InvariantCultureIgnoreCase);
        }
        #region Request

        public string CreateRequestUrl(string baseUrl, string vnp_HashSecret)
        {
            StringBuilder data = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in _requestData)
            {
                if (!String.IsNullOrEmpty(kv.Value))
                {
                    data.Append(WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&");
                }
            }
            string queryString = data.ToString();

            baseUrl += "?" + queryString;
            String signData = queryString;
            if (signData.Length > 0)
            {

                signData = signData.Remove(data.Length - 1, 1);
            }
            string vnp_SecureHash = Utils.HmacSHA512(vnp_HashSecret, signData);
            baseUrl += "vnp_SecureHash=" + vnp_SecureHash;

            return baseUrl;
        }



        #endregion


    }
    public class Utils
    {


        public static String HmacSHA512(string key, String inputData)
        {
            var hash = new StringBuilder();
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }

            return hash.ToString();
        }
        public static string GetIpAddress()
        {
            string ipAddress = "";
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {

                        ipAddress = ip.ToString();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                ipAddress = "Invalid IP:" + ex.Message;
            }

            return ipAddress;
        }
    }

    public class VnPayCompare : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x == y) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            var vnpCompare = CompareInfo.GetCompareInfo("en-US");
            return vnpCompare.Compare(x, y, CompareOptions.Ordinal);
        }
    }
}
