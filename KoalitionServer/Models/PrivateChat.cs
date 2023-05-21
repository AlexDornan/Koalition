namespace KoalitionServer.Models
{
    public class PrivateChat
    {
        public int PrivateChatId { get; set; }
        //public ICollection<User> Participants { get; set; }
        public ICollection<PrivateMessage> Messages { get; set; }
        public ICollection<PrivateChatsToUsers> PrivateChatsToUsers { get; set; }
    }
}
