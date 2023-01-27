using ControleDeContatos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDeContatos.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
        }

        public DbSet<ContatoModel> Contatos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ContatoModel>()
                .HasKey(c => c.Id);

            builder.Entity<ContatoModel>()
                .Property(c => c.Nome)
                .HasMaxLength(100);

            builder.Entity<ContatoModel>()
                .Property(c => c.Email)
                .HasMaxLength(150);

            builder.Entity<ContatoModel>()
                .Property(c => c.Celular);
        }
    }
}
