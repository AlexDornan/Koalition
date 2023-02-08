using Microsoft.AspNetCore.Identity;

using System.ComponentModel.DataAnnotations;

namespace KoalitionServer.Models
{
    public class User : IdentityUser
    {
        public string UserID { get; set; }
        [Required]
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Online { get; set; }
        public DateTime LastRescent { get; set; }

        public User() 
        { 
            Messages = new HashSet<Message>();
        }

        //one user to many messages
        public virtual ICollection<Message> Messages { get; set; }
        public ICollection<GroupChat> GroupChats { get; set; }
        public ICollection<PrivateChat> PrivateChats { get; set; }
    }
}
