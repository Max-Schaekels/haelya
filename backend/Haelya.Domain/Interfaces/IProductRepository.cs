using Haelya.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task AddAsync(Product product);
        Task ArchiveAsync(int id);

        // Updates spécifiques
        Task UpdateInfosAsync(Product product); 
        Task UpdatePriceAsync(int productId, decimal newPrice);
        Task UpdateMarginAsync(int productId, decimal newMargin);

        // Updates booléens individuels
        Task SetActiveAsync(int id, bool isActive);
        Task SetFeaturedAsync(int id, bool featured);
        Task SetInSlideAsync(int id, bool inSlide);
    }
}
