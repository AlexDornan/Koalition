using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class PrivateMessage
    {   
        public int PrivateMessageId { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public int PrivateChatId { get; set; }
        public PrivateChat PrivateChat { get; set; }
        public int UserId { get; set; }
        public PrivateMessage()
        {
            Time = DateTime.Now;
        }
    }
}
