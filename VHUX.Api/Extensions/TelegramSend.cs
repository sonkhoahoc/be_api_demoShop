using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;

namespace VHUX.Api.Extensions
{
    public class TelegramSend
    {
        private readonly TeleGramPush _teleGramPush;
        public TelegramSend(IOptions<TeleGramPush> teleGramPush)
        {
            _teleGramPush = teleGramPush.Value;

        }
        public string TelegramSendMessage(TelegramModel model)
        {
            try
            {
                string body = JsonConvert.SerializeObject(model);
                string urlString = $"https://api.telegram.org/bot{_teleGramPush.token}/sendMessage?chat_id={_teleGramPush.channel}&text={body}";

                WebClient webclient = new WebClient();

                return webclient.DownloadString(urlString);

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
    public class TelegramModel
    {
        public string note { get; set; }
        public string title { get; set; }
    }
    public class TeleGramPush
    {
        public string token { set; get; }
        public string channel { set; get; }
        public string url { set; get; }
    }
}
