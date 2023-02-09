using Microsoft.AspNetCore.Identity;

using System.ComponentModel.DataAnnotations;

namespace KoalitionServer.Models
{
    public class User : IdentityUser
    {
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Online { get; set; }
        public DateTime LastRescent { get; set; }

        public User() 
        { 
            PrivateMessages = new HashSet<PrivateMessage>();
            GroupMessages = new HashSet<GroupMessage>();
        }

        //one user to many messages
        public virtual ICollection<PrivateMessage> PrivateMessages { get; set; }
        public virtual ICollection<GroupMessage> GroupMessages { get; set; }
        public ICollection<GroupChat> GroupChats { get; set; }
        public ICollection<PrivateChat> PrivateChats { get; set; }
    }
}
