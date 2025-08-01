using Favorite.Products.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Favorite.Products.Infra.Data.Configuration
{
    public class FavoriteProductConfiguration : IEntityTypeConfiguration<FavoriteProduct>
    {
        public void Configure(EntityTypeBuilder<FavoriteProduct> builder)
        {
            builder.ToTable("favorite_product");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .IsRequired(true);

            builder.Property(x => x.ProductId)
                .HasColumnName("product_id")
                .IsRequired(true);

            builder.Property(x => x.Title)
                .HasColumnName("title")
                .HasColumnType("varchar(100)")
                .IsRequired(false);

            builder.Property(x => x.UrlImage)
                .HasColumnName("url_image")
                .HasColumnType("varchar(255)")
                .IsRequired(false);

            builder.Property(x => x.Price)
                .HasColumnName("price")
                .HasColumnType("decimal(10,2)")
                .IsRequired(true);

            builder.Property(x => x.Review)
                .HasColumnName("review")
                .HasColumnType("varchar(500)")
                .IsRequired(false);

            builder.Property(x => x.DateCreated)
                .HasColumnName("date_created")
                .HasColumnType("timestamp without time zone")
                .HasDefaultValueSql("now()")
                .IsRequired(true);

            builder.Property(x => x.CustomerId)
                .HasColumnName("customer_id")
                .IsRequired(true);

            builder.HasOne(x => x.Customer)
                .WithMany(c => c.FavoriteProducts)
                .HasForeignKey(x => x.CustomerId);

            builder.HasIndex(f => f.CustomerId)
                .HasDatabaseName("ix_favorite_product_customer_id");
        }
    }

}
