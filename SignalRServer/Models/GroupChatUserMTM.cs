namespace KoalitionServer.Models
{
    public class GroupChatUserMTM
    {
        public int GroupChatUserMTMId { get; set; }
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public bool IsOwner { get; set; }
    }
}
