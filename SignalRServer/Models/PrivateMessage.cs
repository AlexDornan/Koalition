using System.ComponentModel.DataAnnotations;

namespace KoalitionServer.Models
{
    public class PrivateMessage
    {   
        public int PrivateMessageId { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public int PrivateChatId { get; set; }
        public PrivateMessage()
        {
            Time = DateTime.Now;
        }
    }
}
