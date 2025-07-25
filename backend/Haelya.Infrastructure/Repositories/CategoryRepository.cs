using Haelya.Application.Exceptions;
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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly HaelyaDbContext _context;

        public CategoryRepository(HaelyaDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task DisableAsync(int id)
        {
            Category? category = await _context.Categories.FindAsync(id);
            if (category == null) 
            {
                throw new KeyNotFoundException("Category not found");
            }
            category.IsActive = false;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.Categories.AnyAsync(c => c.Name == name);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories
                .Where(c => c.IsActive)
                .ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateAsync(Category category)
        {
            if (category is null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            if (!await _context.Categories.AnyAsync(c => c.Id == category.Id))
            {
                throw new KeyNotFoundException("Category not found");
            }
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
