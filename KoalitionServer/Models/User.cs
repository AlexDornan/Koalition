using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KoalitionServer.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? Online { get; set; }
        public DateTime? LastRescent { get; set; }

        //one user to many messages and groups
        public ICollection<PrivateMessage> PrivateMessages { get; set; }
        //public ICollection<PrivateMessage> ReceivedPrivateMessages { get; set; }
        public ICollection<GroupMessage> GroupMessages { get; set; }
        public ICollection<GroupChatsToUsers> GroupChatsToUsers { get; set; }
        public ICollection<PrivateChat> PrivateChats { get; set; }
        public ICollection<PrivateChatsToUsers> PrivateChatsToUsers { get; set; }

    }
}
