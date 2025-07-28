using Haelya.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllVisibleAsync();
        Task<IEnumerable<ProductDTO>> GetAllAdminAsync();
        Task<ProductDTO?> GetByIdAsync(int id);
        Task<ProductDTO> CreateAsync(ProductCreateDTO dto);
        Task<ProductDTO?> GetBySlugAsync(string slug);

        Task UpdateAsync(int id, ProductUpdateDTO dto);
        Task UpdatePriceAsync(ProductUpdatePriceDTO dto);
        Task UpdateMarginAsync(ProductUpdateMarginDTO dto);

        Task SetActiveAsync(int id, bool isActive);
        Task SetFeaturedAsync(int id, bool featured);
        Task SetInSlideAsync(int id, bool inSlide);


    }
}
