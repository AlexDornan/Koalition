﻿namespace KoalitionServer.Models
{
    public class PrivateChat
    {
        public int PrivateChatId { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
