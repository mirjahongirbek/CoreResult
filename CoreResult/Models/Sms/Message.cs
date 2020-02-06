using Newtonsoft.Json;

namespace CoreResult.Models.Sms
{
    public class Message
    {
        public string recipient { get; set; }
        [JsonProperty("message-id")]
        public string message_id { get; set; }

        public string priority { get; set; }
        public SmsModel sms { get; set; }
    }
}
