namespace LineMessaging.Model
{
    public class SocialMessageModel
    {
        public Guid? SocialMessageId { get; set; }
        public Guid AccountId { get; set; }
        public string ThirdPartyId { get; set; }
        public short ChanelType { get; set; }
        public string LeadsThirdPartyId { get; set; }
        public string MessageId { get; set; }
        public short MessageType { get; set; }
        public string Text { get; set; }
        public string StickerId { get; set; }
        public string FileUrl { get; set; }
        public string FileName { get; set; }
        public string ConnectionId { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? Timestamp { get; set; }

    }

}
