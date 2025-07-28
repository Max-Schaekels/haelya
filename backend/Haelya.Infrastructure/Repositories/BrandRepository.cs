using Haelya.Domain.Entities;
using Haelya.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Infrastructure.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly HaelyaDbContext _context;

        public BrandRepository(HaelyaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Brand>> GetAllVisibleAsync()
        {
            return await _context.Brands
                .Where(b => b.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Brand>> GetAllAdminAsync()
        {
            return await _context.Brands.ToListAsync();
        }

        public async Task<Brand?> GetByIdAsync(int id)
        {
            return await _context.Brands
                .Where(b => b.IsActive && b.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(Brand brand)
        {
            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Brand brand)
        {
            _context.Brands.Update(brand);
            await _context.SaveChangesAsync();
        }

        public async Task SetActiveAsync(int id, bool isActive)
        {
            Brand? brand = await _context.Brands.FindAsync(id);
            if (brand is null)
            {
                throw new KeyNotFoundException("Brand not found");
            }

            brand.IsActive = isActive;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Brands
                .AnyAsync(b => b.Id == id);
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.Brands
                .AnyAsync(b => b.Name == name);
        }
    }
}
