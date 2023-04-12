using WindowsFormsApp1.Users;

namespace WindowsFormsApp1.Models
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public User userDetails { get; set; }
    }
}
