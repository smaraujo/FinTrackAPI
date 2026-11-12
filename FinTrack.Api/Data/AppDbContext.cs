using FinTrack.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Transacao> Transacoes => Set<Transacao>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.ToTable("Categorias");
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Nome).IsRequired().HasMaxLength(80);
            entity.Property(c => c.Cor).HasMaxLength(20);
            entity.HasIndex(c => c.Nome).IsUnique();
        });

        modelBuilder.Entity<Transacao>(entity =>
        {
            entity.ToTable("Transacoes");
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Descricao).IsRequired().HasMaxLength(160);
            entity.Property(t => t.Valor).HasColumnType("decimal(18,2)");
            entity.Property(t => t.Tipo).HasConversion<int>();
            entity.Property(t => t.Data).IsRequired();

            entity.HasOne(t => t.Categoria)
                .WithMany(c => c.Transacoes)
                .HasForeignKey(t => t.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
