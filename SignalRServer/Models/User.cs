using Microsoft.AspNetCore.Identity;

using System.ComponentModel.DataAnnotations;

namespace KoalitionServer.Models
{
    public class User : IdentityUser
    {
        public string UserID { get; set; }
        [Required]
        public string Name { get; set; }
        public User() 
        { 
            Messages = new HashSet<Message>();
        }

        //one user to many messages
        public virtual ICollection<Message> Messages { get; set; }
    }
}
