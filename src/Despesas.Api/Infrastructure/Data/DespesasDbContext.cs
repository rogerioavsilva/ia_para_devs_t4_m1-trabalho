using Despesas.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Despesas.Api.Infrastructure.Data;

public class DespesasDbContext(DbContextOptions<DespesasDbContext> options) : DbContext(options)
{
    public DbSet<Despesa> Despesas => Set<Despesa>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Despesa>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.NomeFuncionario).IsRequired().HasMaxLength(200);
            entity.Property(d => d.Tipo).IsRequired();
            entity.Property(d => d.Valor).HasPrecision(18, 2).IsRequired();
            entity.Property(d => d.RegistradaEm).IsRequired();
        });
    }
}
