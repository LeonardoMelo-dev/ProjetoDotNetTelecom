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
        public DbSet<Packets_Services> Packets_Services { get; set; }
        public DbSet<Packets_Users> Packets_Users{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Services>().ToTable("Services").HasKey("ServiceId");
            modelBuilder.Entity<Packets>().ToTable("Packets").HasKey("PacketId");
            modelBuilder.Entity<Users>().ToTable("Users").HasKey("UserId");
            modelBuilder.Entity<Packets_Services>().ToTable("Packets_Services").HasKey(al => new {al.Packets_ServicesId, al.PacketId, al.ServiceId });
            modelBuilder.Entity<Packets_Users>().ToTable("Packets_Users").HasKey(al => new { al.Packets_UsersId, al.PacketId, al.UserId });

        }
    }
}
