using System.ComponentModel.DataAnnotations;

namespace KoalitionServer.Models
{
    public class GroupMessage
    {
        public int GroupMessageId { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public int GroupChatId { get; set; }
        public int UserId { get; set; }
        public GroupMessage()
        {
            Time = DateTime.Now;
        }
    }
}
