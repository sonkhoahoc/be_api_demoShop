using Newtonsoft.Json;
using RestSharp;
using VHUX.Api.Model;

namespace VHUX.Api.Extensions
{
    public class ServiceConnect
    {
        public async Task<string> SendOTP(string phone_number, string otp)
        {
            string sms_Url = "http://api.brandsms.vn/api";
            OTP_BodyModel bodyModel = new OTP_BodyModel();
            bodyModel.message = otp + " la ma xac nhan cua ban tai Smartgap.vn, co hieu luc trong vong 5phut. VUI LONG KHONG CHIA SE MA OTP VOI BAT KI AI";
            bodyModel.to = phone_number;
            try
            {  // Define a resource path
                string resourcePath = "/SMSBrandname/SendSMS";
                var body = JsonConvert.SerializeObject(bodyModel);

                // Define a client
                var client = new RestClient(sms_Url);

                // Define a request
                var request = new RestRequest(resourcePath, Method.Post);
                string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c24iOiJzbWFydGdhcCI" +
                    "sInNpZCI6IjRkYWExYWM3LTBmNDYtNGQ4Mi05MjEzLTEwYmFkNDExNjBkNCIsIm9idCI6IiIsIm9iaiI6IiI" +
                    "sIm5iZiI6MTYxOTE0Nzg5OSwiZXhwIjoxNjE5MTUxNDk5LCJpYXQiOjE2MTkxNDc4OTl9.T-uQrmbf6vv50L6GYCTRq3kuvMouwRuux9rqq_YUdFk";
                // Add headers
                request.AddHeader("Content-Type", "application/json");

                request.AddHeader("Accept", "application/json");
                // request.AddHeader("token",  token);
                request.AddHeader("authorization", "Bearer " + token);
                request.AddParameter("application/json; charset=utf-8", body, ParameterType.RequestBody);
                //  request.AddJsonBody(body);

                var response = client.Execute(request);
                string content = response.Content ?? "";
                var data = JsonConvert.DeserializeObject<responseModel>(content);
                return data.errorCode == "000" ? bodyModel.message : "0";
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return message;
            }

        }
    }
}
