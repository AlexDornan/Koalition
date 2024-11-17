namespace Server.Requests.UserRequests
{
    public class DeleteCurrentUserRequest
    {
        public string Login { get; set; }

        public DeleteCurrentUserRequest(string login)
        {
            Login = login;
        }
    }

}
