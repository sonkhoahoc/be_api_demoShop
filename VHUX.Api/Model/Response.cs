namespace VHUX.Model
{
    public interface IResponseData
    {

    }
    public class PaginationSet<T> where T : class, new()
    {
        public int page { set; get; }

        public int count
        {
            get
            {
                return (list != null) ? list.Count() : 0;
            }
        }
        public int totalpage { set; get; } = 1;
        public int totalcount { set; get; } = 0;
        public int maxpage { set; get; }
        public IEnumerable<T> list { set; get; }
    }
    public class ResponseSingleContentModel<T>
    {    
        public string Message { get; set; } = string.Empty;   
        public int StatusCode { set; get; } = 200;    
        public T? Data { set; get; }
    }
    public class ResponseMultiContentModels<T>
    {  
        public string Message { get; set; } = string.Empty;
        public int StatusCode { set; get; } = 200;    
        public List<T> Data { set; get; }
    }
    public class AppsetingUrl
    {
        public string PosUrl { set; get; }
        public string SaleUrl { set; get; }
        public string CategoryUrl { set; get; }
    }
    public class OTP_BodyModel
    {
        public string to { get; set; }
        public string from { get; set; } = "Smartgap.vn";
        public string message { get; set; }
        public string scheduled { get; set; } = "";
        public string telco { get; set; } = "";
        public string requestId { get; set; } = "";
        public string packageCode { get; set; } = "";
        public int Unicode { get; set; } = 0;
        public int type { get; set; } = 1;


    }
    public class responseModel
    {
        public OTP_BodyModel sendMessage { get; set; }
        public int msgLength { set; get; }
        public int mtCount { set; get; }
        public string? account { set; get; }
        public string? errorCode { set; get; }
        public string? errorMessage { set; get; }
        public string? referentId { set; get; }
    }
  

    public class ConnectServicesSetting
    {
        public string Vnpay_Url { set; get; }
        public string vnp_HashSecret { set; get; }
        public string vnp_TmnCode { set; get; }
        public string vnp_Version { set; get; }
        public string vnp_Locale { set; get; }
        public string vnp_OrderType { set; get; }
        public string vnp_ReturnUrl { set; get; }

    }

}
