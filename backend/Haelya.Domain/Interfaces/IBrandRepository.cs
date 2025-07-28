using Haelya.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Domain.Interfaces
{
    public interface IBrandRepository
    {
        Task<IEnumerable<Brand>> GetAllVisibleAsync();
        Task<IEnumerable<Brand>> GetAllAdminAsync();
        Task<Brand?> GetByIdAsync(int id);
        Task AddAsync(Brand brand);
        Task UpdateAsync(Brand brand);
        Task SetActiveAsync(int id, bool isActive);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsByNameAsync(string name); 
    }
}
