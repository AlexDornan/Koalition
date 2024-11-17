namespace Server.Responses.UserResponses
{
    public class CurrentUserResponse
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
