using Haelya.Application.DTOs.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Application.Interfaces
{
    public interface IBrandService
    {
        Task<BrandDTO> CreateAsync(BrandCreateDTO dto);
        Task UpdateAsync(int id, BrandUpdateDTO dto);
        Task<BrandDTO?> GetByIdAsync(int id);
        Task<IEnumerable<BrandDTO>> GetAllVisibleAsync(); 
        Task<IEnumerable<BrandDTO>> GetAllAdminAsync();   
        Task SetActiveAsync(int id, bool isActive);
    }
}
