using MediatR;

namespace KoalitionServer.Requests.GroupChatRequests
{
    public class AddUserToGroupChatRequest : IRequest<bool>
    {
        public int GroupChatId { get; set; }
        public int UserId { get; set; }
    }
}
