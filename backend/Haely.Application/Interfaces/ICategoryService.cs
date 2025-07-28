using Haelya.Application.DTOs.Category;
using Haelya.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllVisibleAsync();
        Task<IEnumerable<CategoryDTO>> GetAllAdminAsync();
        Task<CategoryDTO?> GetByIdAsync(int id);
        Task<CategoryDTO> CreateAsync(CategoryCreateDTO dto);
        Task UpdateAsync(int id, CategoryUpdateDTO dto);
        Task SetActiveAsync(int id, bool isActive);
    }
}
