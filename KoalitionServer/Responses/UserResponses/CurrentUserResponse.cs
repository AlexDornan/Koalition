namespace KoalitionServer.Responses.UserResponses
{
    public class CurrentUserResponse
    {
        public class UserContainer
        {
            public int UserId { get; set; }
            public string Login { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
        }

        public UserContainer User { get; set; }
    }
}
