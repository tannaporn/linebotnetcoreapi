using Newtonsoft.Json;

namespace LineMessaging.Model
{
   

    public class LineWebhookEvents
    {
        [JsonProperty("destination")]
        public string Destination { get; set; }

        [JsonProperty("events")]
        public List<LineEvent> Events { get; set; }
    }

    public class LineEvent
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("message")]
        public LineMessage Message { get; set; }

        [JsonProperty("webhookEventId")]
        public string WebhookEventId { get; set; }

        [JsonProperty("deliveryContext")]
        public DeliveryContext DeliveryContext { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("source")]
        public LineSource Source { get; set; }

        [JsonProperty("replyToken")]
        public string ReplyToken { get; set; }

        [JsonProperty("mode")]
        public string Mode { get; set; }
    }

    public class LineMessage
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("quoteToken")]
        public string QuoteToken { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class DeliveryContext
    {
        [JsonProperty("isRedelivery")]
        public bool IsRedelivery { get; set; }
    }

    public class LineSource
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }
    }

}
