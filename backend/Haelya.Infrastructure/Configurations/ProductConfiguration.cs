using Haelya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(p => p.Description)
                .HasMaxLength(1024);

            builder.Property(p => p.ImageUrl)
                .HasMaxLength(1024);

            builder.Property(p => p.Price)
                .HasColumnType("decimal(10,2)");

            builder.Property(p => p.SupplierPrice)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(p => p.Stock)
                .HasDefaultValue(0);

            builder.Property(p => p.Slug)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasIndex(p => p.Slug).IsUnique();

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            builder.HasOne(p => p.Brand)
               .WithMany(b => b.Products)
               .HasForeignKey(p => p.BrandId);

            builder.Property(p => p.Margin)
               .HasColumnType("decimal(10,2)")
               .IsRequired();

            builder.Property(p => p.ViewCount)
                .HasDefaultValue(0);

            builder.Property(p => p.TotalNote)
                .HasDefaultValue(0);

            builder.Property(p => p.NoteCount)
                .HasDefaultValue(0);

            builder.Property(p => p.InSlide)
                .HasDefaultValue(false);

            builder.Property(p => p.IsActive)
                .HasDefaultValue(false);

            builder.Property(p => p.Featured)
                .HasDefaultValue(false);


            builder.Property(p => p.DateCreated)
                .HasColumnType("datetime2");

            builder.Property(p => p.DateUpdated)
                .HasColumnType("datetime2");


        }
    }
}
