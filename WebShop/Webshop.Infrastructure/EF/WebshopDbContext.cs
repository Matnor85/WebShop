using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Entitites;
using Webshop.Infrastructure.EF.Configurations;

namespace Webshop.Infrastructure.EF;

public class WebshopDbContext : DbContext
{
  public  DbSet<Produkt> Produkter { get; set; }
    public DbSet<Kund> Kunder { get; set; }
    public DbSet<Leverantör> Leverantörer { get; set; }
    public DbSet<Kategori> Kategorier { get; set; }
    public DbSet<Order> Ordrar { get; set; }
    public DbSet<ProduktOrder> ProduktOrdrar { get; set; }
    private string ConnectionString { get; set; }
   
    public WebshopDbContext(DbContextOptions<WebshopDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebshopDbContext).Assembly);
    }
}
