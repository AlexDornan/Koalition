using KoalitionServer.Responses.UserResponses;
using MediatR;

namespace KoalitionServer.Requests.UserRequests
{
    public class CurrentUserRequestDTO : IRequest<CurrentUserResponseDTO>
    {
        public CurrentUserRequestDTO(string login)
        {
            Login = login;
        }
        public string Login { get; set; }
    }
}
