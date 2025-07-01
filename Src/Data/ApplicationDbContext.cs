using Microsoft.EntityFrameworkCore;
using TesteTecnicoEffecti.Src.Models;

namespace TesteTecnicoEffecti.Src.Data;

public class ApplicationDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql("server=localhost;port=3306;database=tasks;uid=root;pwd=123", ServerVersion.Parse("5.7.42-mysql"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public virtual DbSet<Licitacao> Licitacoes { get; set; }
}
