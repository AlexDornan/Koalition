namespace Server.Models
{
    public class PrivateChatsToUsers
    {
        public int PrivateChatId { get; set; }
        public PrivateChat PrivateChat { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
