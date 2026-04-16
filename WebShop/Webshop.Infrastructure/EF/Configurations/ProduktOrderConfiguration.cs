using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webshop.Domain.Entitites;

namespace Webshop.Infrastructure.EF.Configurations;

public class ProduktOrderConfiguration : IEntityTypeConfiguration<ProduktOrder>
{
    public void Configure(EntityTypeBuilder<ProduktOrder> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Produkt)
            .WithMany(x => x.ProduktOrdrar)
            .HasForeignKey(x => x.ProduktId);

        builder.Property(x => x.PrisvidKöp)
               .HasColumnType("decimal(18,2)");
    }
}