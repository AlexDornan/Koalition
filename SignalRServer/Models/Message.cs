using System.ComponentModel.DataAnnotations;

namespace KoalitionServer.Models
{
    public class Message
    {   
        public int MessageId { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public int GroupChatId { get; set; }
        public int PrivateChatId { get; set; }
        public virtual User Sender { get; set; }
        public Message()
        {
            Time = DateTime.Now;
        }

    }
}
