using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KoalitionServer.Models;
using KoalitionServer.Constants;

namespace KoalitionServer.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
            { }

        /*protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Message>()
                .HasOne<User>(a => a.UserId)
                .WithMany(d => d.Messages)
                .HasForeignKey(a => a.UserID);
        }*/

        public DbSet<GroupMessage> GroupMessages { get; set; }
        public DbSet<PrivateMessage> PrivateMessages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<GroupChat> GroupChats{ get; set; }
        public DbSet<PrivateChat> PrivateChats { get; set; }
        public DbSet<GroupChatUserMTM> GroupChatUserMTM { get; set; }
        public DbSet<PrivateChatUserMTM> PrivateChatUserMTM { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public AppDbContext() => Database.EnsureCreated();
    }
}
