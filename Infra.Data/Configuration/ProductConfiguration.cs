using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product", "PAYMENTS");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").ValueGeneratedNever()
            .IsRequired();
        builder.Property(x => x.StripeId).HasColumnName("StripeId").HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
        builder.Property(x => x.Name).HasColumnName("Name").HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
        builder.Property(x => x.Description).HasColumnName("Description").HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
        builder.Property(x => x.Platform).HasColumnName("Platform").HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
    }
}