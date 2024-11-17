namespace Server.Requests.GroupChatRequests
{
    public class AddUserToGroupChatRequest
    {
        public int GroupChatId { get; set; }
        public int UserId { get; set; }
    }
}
