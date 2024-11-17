using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
            { }

        public DbSet<GroupMessage> GroupMessages { get; set; }
        public DbSet<PrivateMessage> PrivateMessages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<GroupChat> GroupChats{ get; set; }
        public DbSet<PrivateChat> PrivateChats { get; set; }
        public DbSet<GroupChatsToUsers> GroupChatsToUsers { get; set;}
        public DbSet<PrivateChatsToUsers> PrivateChatsToUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupChatsToUsers>()
                .HasKey(gcu => new { gcu.GroupChatId, gcu.UserId });

            modelBuilder.Entity<GroupChatsToUsers>()
                .HasOne(gcu => gcu.GroupChat)
                .WithMany(g => g.GroupChatsToUsers)
                .HasForeignKey(gcu => gcu.GroupChatId);

            modelBuilder.Entity<GroupChatsToUsers>()
                .HasOne(gcu => gcu.User)
                .WithMany(u => u.GroupChatsToUsers)
                .HasForeignKey(gcu => gcu.UserId);

            modelBuilder.Entity<GroupChatsToUsers>()
                .Property(gcu => gcu.IsOwner)
                .IsRequired();



            modelBuilder.Entity<PrivateChatsToUsers>()
                .HasKey(pcu => new { pcu.PrivateChatId, pcu.UserId });

            modelBuilder.Entity<PrivateChatsToUsers>()
                .HasOne(pcu => pcu.PrivateChat)
                .WithMany(p => p.PrivateChatsToUsers)
                .HasForeignKey(pcu => pcu.PrivateChatId);

            modelBuilder.Entity<PrivateChatsToUsers>()
                .HasOne(pcu => pcu.User)
                .WithMany(u => u.PrivateChatsToUsers)
                .HasForeignKey(pcu => pcu.UserId);
        }
    }
}
