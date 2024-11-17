﻿namespace Server.Models
{
    public class GroupChatsToUsers
    {
        public int GroupChatId { get; set; }
        public GroupChat GroupChat { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public bool IsOwner { get; set; }
    }
}
