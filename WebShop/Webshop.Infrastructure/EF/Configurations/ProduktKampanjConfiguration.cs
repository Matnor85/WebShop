using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webshop.Domain.Entitites;

namespace Webshop.Infrastructure.EF.Configurations;

public class ProduktKampanjConfiguration : IEntityTypeConfiguration<ProduktKampanj>
{
    public void Configure(EntityTypeBuilder<ProduktKampanj> builder)
    {
        builder.HasKey(pk => pk.Id);

        builder.Property(pk => pk.Rabatt)
               .HasColumnType("decimal(5,2)")
               .IsRequired();

        builder.HasOne(pk => pk.Produkt)
               .WithMany()
               .HasForeignKey(pk => pk.ProduktId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}