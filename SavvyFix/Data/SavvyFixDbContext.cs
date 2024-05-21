using Microsoft.EntityFrameworkCore;
using SavvyFix.Models;

namespace SavvyFix.Data;

/*
 * Classe para configurar e mapear operações com o banco de dados
 */

public class SavvyFixDbContext : DbContext
{
    public DbSet<Produtos> Produtos { get; set; }
    public DbSet<Clientes> Clientes { get; set; }
    
    public DbSet<Atividade> Atividades { get; set; }
    public DbSet<Compras> Compra { get; set; }
    

    public SavvyFixDbContext(DbContextOptions<SavvyFixDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Atividade>()
            .Property(a => a.PrecoVariado)
            .HasColumnType("decimal(18,2)");
        
        modelBuilder.Entity<Compras>()
            .Property(c => c.ValorCompra)
            .HasColumnType("decimal(18,2)");
        
        modelBuilder.Entity<Produtos>()
            .Property(p => p.PrecoFixo)
            .HasColumnType("decimal(18,2)");
        
        modelBuilder.Entity<Compras>()
            .HasKey(c => c.IdCompra);

        base.OnModelCreating(modelBuilder);
    }
    
    
    
    
}    