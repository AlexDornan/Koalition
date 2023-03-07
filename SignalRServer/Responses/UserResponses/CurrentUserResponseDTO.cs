namespace KoalitionServer.Responses.UserResponses
{
    public class CurrentUserResponseDTO
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
