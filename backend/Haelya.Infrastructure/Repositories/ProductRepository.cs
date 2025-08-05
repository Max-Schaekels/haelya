using AutoMapper.Features;
using Haelya.Domain.Common;
using Haelya.Domain.Entities;
using Haelya.Domain.Filters;
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


        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Products.AnyAsync(p => p.Id == id);  
        }

        public async Task<PagedResult<Product>> GetAllVisibleAsync(PaginationQuery pagination)
        {
            IQueryable<Product> query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Where(p => p.IsActive);

            int total = await query.CountAsync();
            List<Product> items = await query
                .Skip(pagination.Skip)
                .Take(pagination.PageSize)
                .ToListAsync();

            return new PagedResult<Product>
            {
                Items = items,
                TotalCount = total,
                Page = pagination.Page,
                PageSize = pagination.PageSize
            };
        }

        public async Task<PagedResult<Product>> GetAllAdminAsync(PaginationQuery pagination)
        {
            IQueryable<Product> query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand);

            int total = await query.CountAsync();
            List<Product> items = await query
                .Skip(pagination.Skip)
                .Take(pagination.PageSize)
                .ToListAsync();

            return new PagedResult<Product>
            {
                Items = items,
                TotalCount = total,
                Page = pagination.Page,
                PageSize = pagination.PageSize
            };
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

        public async Task<Product?> GetBySlugAsync(string slug)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .FirstOrDefaultAsync(p => p.Slug == slug && p.IsActive);
        }

        public async Task<PagedResult<Product>> GetFilteredVisibleAsync(ProductFilterPublic filter)
        {
            IQueryable<Product> query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Where(p => p.IsActive);

            query = ApplyFilters(query, filter);
            query = ApplySortingPublic(query, filter.SortBy, filter.SortDirection == "asc");

            int totalCount = await query.CountAsync();
            List<Product> items = await query
                .Skip(filter.Skip)
                .Take(filter.PageSize)
                .ToListAsync();


            return new PagedResult<Product>
            {
                Items = items,
                TotalCount = totalCount,
                Page = filter.Page,
                PageSize = filter.PageSize
            };
        }

        public async Task<PagedResult<Product>> GetFilteredAdminAsync(ProductFilterAdmin filter)
        {
            IQueryable<Product> query = _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Brand);


            query = ApplyFilters(query, filter);
            query = ApplySortingAdmin(query, filter.SortBy, filter.SortDirection == "asc");

            int totalCount = await query.CountAsync(); 
            List<Product> items = await query
                .Skip(filter.Skip)
                .Take(filter.PageSize)
                .ToListAsync();

            return new PagedResult<Product>
            {
                Items = items,
                TotalCount = totalCount,
                Page = filter.Page,
                PageSize = filter.PageSize
            };
        }

        // Méthodes privées pour appliquer les filtres
        private IQueryable<Product> ApplyFilters(IQueryable<Product> query, ProductFilterPublic filter)
        {
            if (filter.CategoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == filter.CategoryId.Value);
            }

            if (filter.BrandId.HasValue)
            {
                query = query.Where(p => p.BrandId == filter.BrandId.Value);
            }

            if (filter.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= filter.MinPrice.Value);
            }

            if (filter.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);
            }

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                string search = filter.Search.Trim();
                query = query.Where(p => p.Name.Contains(search) || p.Description.Contains(search));
            }

            return query;
        }

        private IQueryable<Product> ApplyFilters(IQueryable<Product> query, ProductFilterAdmin filter)
        {
            query = ApplyFilters(query, filter); 

            if (filter.IsActive.HasValue)
            {
                query = query.Where(p => p.IsActive == filter.IsActive.Value);
            }

            return query;
        }

        // Méthode pour trier les produits
        private IQueryable<Product> ApplySortingPublic(IQueryable<Product> query, string? sortBy, bool isAscending)
        {
            string key = sortBy?.Trim().ToLowerInvariant() ?? string.Empty;

            if (key == "price")
                return isAscending ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price);

            if (key == "name")
                return isAscending ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);

            if (key == "datecreated")
                return isAscending ? query.OrderBy(p => p.DateCreated) : query.OrderByDescending(p => p.DateCreated);

            if (key == "viewcount")
                return isAscending ? query.OrderBy(p => p.ViewCount) : query.OrderByDescending(p => p.ViewCount);

            if (key == "avgnote")
                return isAscending ? query.OrderBy(p => p.AvgNote) : query.OrderByDescending(p => p.AvgNote);

            // Tri par défaut
            return query.OrderBy(p => p.Name);
        }

        private IQueryable<Product> ApplySortingAdmin(IQueryable<Product> query, string? sortBy, bool isAscending)
        {
            string key = sortBy?.Trim().ToLowerInvariant() ?? string.Empty;

            if (key == "price")
                return isAscending ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price);

            if (key == "name")
                return isAscending ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);

            if (key == "datecreated")
                return isAscending ? query.OrderBy(p => p.DateCreated) : query.OrderByDescending(p => p.DateCreated);

            if (key == "dateupdated")
                return isAscending ? query.OrderBy(p => p.DateUpdated) : query.OrderByDescending(p => p.DateUpdated);

            if (key == "stock")
                return isAscending ? query.OrderBy(p => p.Stock) : query.OrderByDescending(p => p.Stock);

            if (key == "slug")
                return isAscending ? query.OrderBy(p => p.Slug) : query.OrderByDescending(p => p.Slug);

            if (key == "supplierprice")
                return isAscending ? query.OrderBy(p => p.SupplierPrice) : query.OrderByDescending(p => p.SupplierPrice);

            if (key == "margin")
                return isAscending ? query.OrderBy(p => p.Margin) : query.OrderByDescending(p => p.Margin);

            if (key == "isactive")
                return isAscending ? query.OrderBy(p => p.IsActive) : query.OrderByDescending(p => p.IsActive);

            if (key == "featured")
                return isAscending ? query.OrderBy(p => p.Featured) : query.OrderByDescending(p => p.Featured);

            if (key == "inslide")
                return isAscending ? query.OrderBy(p => p.InSlide) : query.OrderByDescending(p => p.InSlide);

            if (key == "totalnote")
                return isAscending ? query.OrderBy(p => p.TotalNote) : query.OrderByDescending(p => p.TotalNote);

            if (key == "notecount")
                return isAscending ? query.OrderBy(p => p.NoteCount) : query.OrderByDescending(p => p.NoteCount);

            if (key == "viewcount")
                return isAscending ? query.OrderBy(p => p.ViewCount) : query.OrderByDescending(p => p.ViewCount);

            if (key == "avgnote")
                return isAscending ? query.OrderBy(p => p.AvgNote) : query.OrderByDescending(p => p.AvgNote);

            if (key == "id")
                return isAscending ? query.OrderBy(p => p.Id) : query.OrderByDescending(p => p.Id);

            // Tri par défaut
            return query.OrderBy(p => p.Name);
        }
    }
}
