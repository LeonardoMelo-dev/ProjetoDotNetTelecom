using Microsoft.EntityFrameworkCore;
using ProjetoTelecon.Models;

namespace ProjetoTelecon.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<Servico> Servico { get; set; }
        public DbSet<Pacote> Pacote { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Servico>().ToTable("Servico").HasKey("ServicoId");
            modelBuilder.Entity<Pacote>().ToTable("Pacote").HasKey("PacoteId");
            modelBuilder.Entity<Usuario>().ToTable("Usuario").HasKey("UsuarioId");

        }
    }
}
