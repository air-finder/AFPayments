using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Configuration;

public class PriceConfiguration : IEntityTypeConfiguration<Price>
{
    public void Configure(EntityTypeBuilder<Price> builder)
    {
        builder.ToTable("Price", "PAYMENTS");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").ValueGeneratedNever()
            .IsRequired();
        builder.Property(x => x.StripeId).HasColumnName("StripeId").HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
        builder.Property(x => x.ProductId).HasColumnName("ProductId").HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
        builder.Property(x => x.UnitAmount).HasColumnName("UnitAmount").HasColumnType("bigint").IsRequired();
        builder.Property(x => x.Currency).HasColumnName("Currency").HasColumnType("nvarchar").HasMaxLength(3).IsRequired();
        builder.Property(x => x.Platform).HasColumnName("Platform").HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
    }
}