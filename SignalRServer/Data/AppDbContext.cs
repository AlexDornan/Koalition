using Microsoft.EntityFrameworkCore;
using KoalitionServer.Models;

namespace KoalitionServer.Data
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
    }
}
