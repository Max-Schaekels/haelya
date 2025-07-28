using Haelya.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllVisibleAsync();
        Task<IEnumerable<Category>> GetAllAdminAsync();
        Task<Category?> GetByIdAsync(int id);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task SetActiveAsync(int id, bool isActive);
        Task<bool> ExistsByNameAsync(string name);
    }
}
