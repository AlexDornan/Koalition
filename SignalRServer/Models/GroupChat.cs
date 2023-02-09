namespace KoalitionServer.Models
{
    public class GroupChat
    {
        public int GroupChatId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<PrivateMessage> Messages { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
