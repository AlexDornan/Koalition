using MediatR;

namespace KoalitionServer.Requests.UserRequests
{
    public class DeleteCurrentUserRequest : IRequest<bool>
    {
        public string Login { get; set; }

        public DeleteCurrentUserRequest(string login)
        {
            Login = login;
        }
    }

}
