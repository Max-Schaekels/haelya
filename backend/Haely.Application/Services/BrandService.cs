using AutoMapper;
using Haelya.Application.DTOs.Brand;
using Haelya.Application.Exceptions;
using Haelya.Application.Interfaces;
using Haelya.Domain.Entities;
using Haelya.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Application.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public BrandService(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<BrandDTO> CreateAsync(BrandCreateDTO dto)
        {
            if (await _brandRepository.ExistsByNameAsync(dto.Name))
            {
                throw new BrandNameAlreadyUsedException();
            }

            Brand brand = _mapper.Map<Brand>(dto);
            await _brandRepository.AddAsync(brand);

            return _mapper.Map<BrandDTO>(brand);
        }

        public async Task UpdateAsync(int id, BrandUpdateDTO dto)
        {
            Brand? brand = await _brandRepository.GetByIdAsync(id);
            if (brand is null)
            {
                throw new NotFoundException("Marque introuvable");
            }

            _mapper.Map(dto, brand);
            await _brandRepository.UpdateAsync(brand);
        }

        public async Task<BrandDTO?> GetByIdAsync(int id)
        {
            Brand? brand = await _brandRepository.GetByIdAsync(id);
            if (brand == null)
                throw new NotFoundException("Marque introuvable");

            return _mapper.Map<BrandDTO>(brand);
        }

        public async Task<IEnumerable<BrandDTO>> GetAllVisibleAsync()
        {
            IEnumerable<Brand> brands = await _brandRepository.GetAllVisibleAsync();
            return _mapper.Map<IEnumerable<BrandDTO>>(brands);
        }

        public async Task<IEnumerable<BrandDTO>> GetAllAdminAsync()
        {
            IEnumerable<Brand> brands = await _brandRepository.GetAllAdminAsync();
            return _mapper.Map<IEnumerable<BrandDTO>>(brands);
        }

        public async Task SetActiveAsync(int id, bool isActive)
        {
            if (!await _brandRepository.ExistsAsync(id))
            {
                throw new NotFoundException("Marque introuvable");
            }

            await _brandRepository.SetActiveAsync(id, isActive);
        }
    }
}
