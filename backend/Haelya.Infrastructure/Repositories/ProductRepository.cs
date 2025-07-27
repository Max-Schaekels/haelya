using AutoMapper.Features;
using Haelya.Domain.Entities;
using Haelya.Domain.Interfaces;
using Haelya.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly HaelyaDbContext _context;

        public ProductRepository(HaelyaDbContext context)
        {
            _context = context;
        }

        private async Task<Product> GetRequiredProductAsync(int id)
        {
            return await _context.Products.FindAsync(id)
                ?? throw new KeyNotFoundException("Product not found");
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task ArchiveAsync(int id)
        {
            Product product = await GetRequiredProductAsync(id);

            product.IsActive = false;
            product.InSlide = false;
            product.Featured = false;
            product.IsDeleted = true;
            product.DateDeleted = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Products.AnyAsync(p => p.Id == id);  
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task SetActiveAsync(int id, bool isActive)
        {
            Product product = await GetRequiredProductAsync(id);

            product.IsActive = isActive;
            await _context.SaveChangesAsync();

        }

        public async Task SetFeaturedAsync(int id, bool featured)
        {
            Product product = await GetRequiredProductAsync(id);

            product.Featured = featured;
            await _context.SaveChangesAsync();
        }

        public async Task SetInSlideAsync(int id, bool inSlide)
        {
            Product product = await GetRequiredProductAsync(id);

            product.InSlide = inSlide;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateInfosAsync(Product product)
        {
            Product productToUpdate = await GetRequiredProductAsync(product.Id);

            productToUpdate.Name = product.Name;
            productToUpdate.Description = product.Description;
            productToUpdate.ImageUrl = product.ImageUrl;
            productToUpdate.SupplierPrice = product.SupplierPrice;
            productToUpdate.Stock = product.Stock;
            productToUpdate.CategoryId = product.CategoryId;

            await _context.SaveChangesAsync();
            
        }

        public async Task UpdateMarginAsync(int productId, decimal newMargin)
        {
            Product productToUpdate = await GetRequiredProductAsync(productId);

            productToUpdate.Margin = newMargin;
            productToUpdate.Price = ProductPricingHelper.CalculatePrice(productToUpdate.SupplierPrice, newMargin);

            await _context.SaveChangesAsync();
        }

        public async Task UpdatePriceAsync(int productId, decimal newPrice)
        {
            Product productToUpdate = await GetRequiredProductAsync(productId);

            productToUpdate.Price = newPrice;
            productToUpdate.Margin = ProductPricingHelper.CalculateMargin(productToUpdate.SupplierPrice, newPrice);

            await _context.SaveChangesAsync();
        }
    }
}
