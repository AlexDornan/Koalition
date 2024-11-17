namespace Server.Responses.PrivateChatResponses
{
    public class PrivateMessageResponse
    {
        public string Text { get; set; }
        public int SenderId { get; set; }
        public DateTime Time { get; set; }
    }
}
