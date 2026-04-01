using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Entitites;
using Webshop.Infrastructure.EF.Configurations;

namespace Webshop.Infrastructure.EF;

public class WebshopDbContext : DbContext
{
    DbSet<Produkt> Produkter { get; set; }
    DbSet<Kund> Kunder { get; set; }
    DbSet<Leverantör> Leverantörer { get; set; }
    DbSet<Kategori> Kategorier { get; set; }
    DbSet<Order> Ordrar { get; set; }
    DbSet<ProduktOrder> produktOrdrar { get; set; }
    private string ConnectionString { get; set; }
    public WebshopDbContext(string connectionString)
    {
        ConnectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("\"Data Source=(localdb)\\\\MSSQLLocalDB;Database=WebShop;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30\"");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebshopDbContext).Assembly);
    }
}
