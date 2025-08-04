using Haelya.Domain.Common;
using Haelya.Domain.Entities;
using Haelya.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<PagedResult<Product>> GetAllVisibleAsync(PaginationQuery pagination);
        Task<PagedResult<Product>> GetAllAdminAsync(PaginationQuery pagination);
        Task<Product?> GetByIdAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task AddAsync(Product product);
        Task<Product?> GetBySlugAsync(string slug);

        // Updates spécifiques
        Task UpdateInfosAsync(Product product); 
        Task UpdatePriceAsync(int productId, decimal newPrice);
        Task UpdateMarginAsync(int productId, decimal newMargin);

        // Updates booléens individuels
        Task SetActiveAsync(int id, bool isActive);
        Task SetFeaturedAsync(int id, bool featured);
        Task SetInSlideAsync(int id, bool inSlide);

        // Filtrage et pagination

        // Pour le public
        Task<PagedResult<Product>> GetFilteredVisibleAsync(ProductFilterPublic filter);

        // Pour l'admin
        Task<PagedResult<Product>> GetFilteredAdminAsync(ProductFilterAdmin filter);
    }
}
