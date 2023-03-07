namespace KoalitionServer.Models
{
    public class PrivateChatUser
    {
        public int PrivateChatUserId { get; set; }
        public int PrivateChatId { get; set; }
        public PrivateChat PrivateChat { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
