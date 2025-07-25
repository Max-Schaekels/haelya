using Haelya.Domain.Entities;
using Haelya.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Infrastructure.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder) 
        {
            builder.ToTable("Category");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(255);


            builder.Property(c => c.Description)
                .HasMaxLength(500);


            builder.Property(c => c.Slug)
                .HasMaxLength(200);

            builder.HasIndex(c => c.Slug)
                .IsUnique();

            builder.Property(c => c.IsActive)
                .IsRequired();
                


           
        }
    }
}
