using Microsoft.EntityFrameworkCore;
using SavvyFix.Models;

namespace SavvyFix.Data;

public class SavvyFixDbContext : DbContext
{
    public DbSet<Produtos> Produtos { get; set; }
    
    public DbSet<Clientes> Clientes { get; set; }
    
    public DbSet<Enderecos> Endereco { get; set; }
    
    public DbSet<Atividade> Atividades { get; set; }
    
    public DbSet<Compras> Compra { get; set; }
    

    public SavvyFixDbContext(DbContextOptions<SavvyFixDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Atividade>()
            .HasOne(a => a.IdClienteNavigation)
            .WithOne(c => c.Atividade)
            .HasForeignKey<Atividade>(a => a.IdCliente);
        
        modelBuilder.Entity<Compras>()
            .HasKey(c => c.IdCompra);

        base.OnModelCreating(modelBuilder);
    }
    
    
    
    
}    