namespace KoalitionServer.Requests.UserRequests
{
    public class RegistrationRequest
    {
        public string Login { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
