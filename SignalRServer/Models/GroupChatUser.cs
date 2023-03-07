namespace KoalitionServer.Models
{
    public class GroupChatUser
    {
        public int GroupChatUserId { get; set; }
        public int GroupChatId { get; set; }
        public GroupChat GroupChat { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public bool IsOwner { get; set; }
    }
}
