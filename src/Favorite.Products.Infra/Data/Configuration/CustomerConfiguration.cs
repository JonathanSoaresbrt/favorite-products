using Favorite.Products.Domain.Models.Entities;
using Favorite.Products.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Favorite.Products.Infra.Data.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("customer");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .IsRequired(true);

            builder.Property(a => a.Name)
                .HasColumnType("varchar(100)")
                .IsRequired(true);

            builder.Property(a => a.Email)
                .HasConversion(
                    v => v.Value,
                    v => new Email(v)
                )
                .HasColumnType("varchar(150)")
                .IsRequired(true);

            builder.HasIndex(c => c.Email)
                .IsUnique()
                .HasDatabaseName("ix_customer_email");

            builder.Property(a => a.Inactive)
                .IsRequired(true);

            builder.Property(a => a.DateUpdated)
                .HasColumnName("date_updated")
                .HasColumnType("timestamp without time zone")
                .HasDefaultValueSql("now()")
                .IsRequired(true);

            builder.Property(a => a.DateCreated)
                .HasColumnName("date_created")
                .HasColumnType("timestamp without time zone")
                .HasDefaultValueSql("now()")
                .IsRequired(true);

            builder.HasMany(c => c.FavoriteProducts)
                .WithOne(p => p.Customer)
                .HasForeignKey(p => p.CustomerId);

            builder.HasIndex(c => c.Email)
                .IsUnique()
                .HasDatabaseName("ix_customer_email");
        }
    }
}
