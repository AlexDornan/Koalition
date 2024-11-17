namespace Server.Requests.UserRequests
{
    public class CurrentUserRequestDTO
    {
        public CurrentUserRequestDTO(string login)
        {
            Login = login;
        }
        public string Login { get; set; }
    }
}
