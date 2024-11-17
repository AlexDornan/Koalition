namespace Server.Models
{
    public class GroupChat
    {
        public int GroupChatId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<GroupMessage> Messages { get; set; }
        public ICollection<GroupChatsToUsers> GroupChatsToUsers { get; set; }
    }
}
