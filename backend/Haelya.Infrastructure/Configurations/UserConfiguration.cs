﻿using Haelya.Domain.Entities;
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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.HashPassword)
                .IsRequired()
                .HasMaxLength(255);

            
            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(100);


            builder.Property(u => u.PhoneNumber)
                .HasMaxLength(20)
                .IsRequired(false);

            
            builder.Property(u => u.BirthDate)
                .HasColumnType("date");

            
            builder.Property(u => u.RegisterDate)
                .HasColumnType("datetime");


            builder.Property(u => u.Role)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50)
                .HasDefaultValue(Role.Customer);
        }
    }
}
