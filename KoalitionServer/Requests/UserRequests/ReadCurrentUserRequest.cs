using KoalitionServer.Responses.UserResponses;

namespace KoalitionServer.Requests.UserRequests
{
    public class ReadCurrentUserRequest : CurrentUserResponse
    {
        public ReadCurrentUserRequest(string login)
        {
            Login = login;
        }
        public string Login { get; set; }
    }
}
