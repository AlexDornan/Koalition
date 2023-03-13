using MediatR;

namespace KoalitionServer.Requests.GroupChatRequests
{
    public class CreateGroupChatRequest:IRequest<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
