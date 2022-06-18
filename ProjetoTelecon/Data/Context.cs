using Microsoft.EntityFrameworkCore;
using ProjetoTelecon.Models;

namespace ProjetoTelecon.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<Services> Services { get; set; }
        public DbSet<Packets> Packets { get; set; }
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Services>().ToTable("Services").HasKey("ServiceId");
            modelBuilder.Entity<Packets>().ToTable("Packets").HasKey("PacketId");
            modelBuilder.Entity<Users>().ToTable("Users").HasKey("UserId");

        }
    }
}
