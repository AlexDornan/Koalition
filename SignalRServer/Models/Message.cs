using System.ComponentModel.DataAnnotations;

namespace KoalitionServer.Models
{
    public class Message
    {   
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public string UserID { get; set; }
        public virtual User Sender { get; set; }
        public Message()
        {
            Time = DateTime.Now;
        }

    }
}
