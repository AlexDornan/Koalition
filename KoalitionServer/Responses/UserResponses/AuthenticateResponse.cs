namespace KoalitionServer.Responses.UserResponses
{
    public class AuthenticateResponse
    {
        public string Token { get; set; }
        public AuthenticatedUserResponse UserDetails { get; set; }
    }
}
