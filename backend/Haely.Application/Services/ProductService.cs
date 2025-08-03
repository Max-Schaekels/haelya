using AutoMapper;
using AutoMapper.Features;
using Haelya.Application.DTOs.Product;
using Haelya.Application.Exceptions;
using Haelya.Application.Interfaces;
using Haelya.Domain.Entities;
using Haelya.Domain.Filters;
using Haelya.Domain.Interfaces;
using Haelya.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDTO> CreateAsync(ProductCreateDTO dto)
        {
            Product product = _mapper.Map<Product>(dto);
            product.Slug = SlugHelper.GenerateSlug(dto.Name);

            await _productRepository.AddAsync(product);

            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllVisibleAsync()
        {
            IEnumerable<Product> products = await _productRepository.GetAllVisibleAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAdminAsync()
        {
            IEnumerable<Product> products = await _productRepository.GetAllAdminAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO?> GetByIdAsync(int id)
        {
            Product? product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new NotFoundException("Product not found");
            }

            return _mapper.Map<ProductDTO>(product);
        }

        public async Task SetActiveAsync(int id, bool isActive)
        {
            if (!await _productRepository.ExistsAsync(id))
            {
                throw new NotFoundException("Product not found");
            }

            await _productRepository.SetActiveAsync(id, isActive);
        }

        public async Task SetFeaturedAsync(int id, bool featured)
        {
            if (!await _productRepository.ExistsAsync(id))
            {
                throw new NotFoundException("Product not found");
            }

            await _productRepository.SetFeaturedAsync(id, featured);
        }

        public async Task SetInSlideAsync(int id, bool inSlide)
        {
            if (!await _productRepository.ExistsAsync(id))
            {
                throw new NotFoundException("Product not found");
            }

            await _productRepository.SetInSlideAsync(id, inSlide);
        }

        public async Task UpdateAsync(int id, ProductUpdateDTO dto)
        {
            Product? existingProduct = await _productRepository.GetByIdAsync(id);

            if (existingProduct == null)
            {
                throw new NotFoundException("Product not found");
            }

            _mapper.Map(dto, existingProduct);

            await _productRepository.UpdateInfosAsync(existingProduct);
        }

        public async Task UpdateMarginAsync(ProductUpdateMarginDTO dto)
        {
            bool exists = await _productRepository.ExistsAsync(dto.Id);
            if (!exists)
            {
                throw new NotFoundException("Product not found");
            }

            await _productRepository.UpdateMarginAsync(dto.Id, dto.Margin);
        }

        public async Task UpdatePriceAsync(ProductUpdatePriceDTO dto)
        {
            bool exists = await _productRepository.ExistsAsync(dto.Id);
            if (!exists)
            {
                throw new NotFoundException("Product not found");
            }

            await _productRepository.UpdatePriceAsync(dto.Id, dto.Price);
        }

        public async Task<ProductDTO?> GetBySlugAsync(string slug)
        {
            Product? product = await _productRepository.GetBySlugAsync(slug);

            if (product == null)
            {
                throw new NotFoundException("Product not found");
            }

            ProductDTO dto = _mapper.Map<ProductDTO>(product);
            return dto;
        }

        public async Task<List<ProductDTO>> GetFilteredVisibleAsync(ProductFilterPublicDTO filterDto)
        {
            ProductFilterPublic filter = _mapper.Map<ProductFilterPublic>(filterDto);

            List<Product> products = await _productRepository.GetFilteredVisibleAsync(filter);

            List<ProductDTO> result = _mapper.Map<List<ProductDTO>>(products);

            return result;
        }

        public async Task<List<ProductDTO>> GetFilteredAdminAsync(ProductFilterAdminDTO filterDto)
        {
            ProductFilterAdmin filter = _mapper.Map<ProductFilterAdmin>(filterDto);

            List<Product> products = await _productRepository.GetFilteredAdminAsync(filter);

            List<ProductDTO> result = _mapper.Map<List<ProductDTO>>(products);

            return result;
        }
    }
}
