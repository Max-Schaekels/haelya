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

        //Updates

        Task UpdateAsync(int id, ProductUpdateDTO dto);
        Task UpdatePriceAsync(ProductUpdatePriceDTO dto);
        Task UpdateMarginAsync(ProductUpdateMarginDTO dto);

        // Updates booléens individuels

        Task SetActiveAsync(int id, bool isActive);
        Task SetFeaturedAsync(int id, bool featured);
        Task SetInSlideAsync(int id, bool inSlide);

        // Filtrage 

        Task<List<ProductDTO>> GetFilteredVisibleAsync(ProductFilterPublicDTO filter);
        Task<List<ProductDTO>> GetFilteredAdminAsync(ProductFilterAdminDTO filter);


    }
}
