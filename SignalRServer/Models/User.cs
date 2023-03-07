using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KoalitionServer.Models
{
    public class User
    {
        public int UserId { get; set; }
        //[Required(ErrorMessage = "Login is required")]
        public string Login { get; set; }
        public string Name { get; set; }
        //[Required(ErrorMessage = "Email is required")]
        //[EmailAddress]
        public string Email { get; set; }
        //[Required(ErrorMessage = "Password is required")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }
        public bool? Online { get; set; }
        public DateTime? LastRescent { get; set; }
        //public string? Token { get; set; }

        //one user to many messages
        public ICollection<PrivateMessage> PrivateMessages { get; set; }
        public ICollection<GroupMessage> GroupMessages { get; set; }
        public ICollection<GroupChat> GroupChats { get; set; }
        public ICollection<PrivateChat> PrivateChats { get; set; }
    }
}
