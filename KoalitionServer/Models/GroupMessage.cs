using System.ComponentModel.DataAnnotations;

namespace KoalitionServer.Models
{
    public class GroupMessage
    {
        public int GroupMessageId { get; set; }
        //[Required]
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public int GroupChatId { get; set; }
        public int UserId { get; set; }
        //public string Name { get; set; }
        public GroupMessage()
        {
            Time = DateTime.Now;
        }
    }
}
