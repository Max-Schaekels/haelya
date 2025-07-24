using Haelya.Domain.Entities;
using Haelya.Domain.Entities.Auth;
using Haelya.Infrastructure.Configurations;
using Haelya.Infrastructure.Configurations.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Infrastructure
{
    public class HaelyaDbContext : DbContext
    {
        public HaelyaDbContext(DbContextOptions<HaelyaDbContext> options) : base(options) 
        { 
        }

        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
