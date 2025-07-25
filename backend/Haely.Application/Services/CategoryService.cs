using AutoMapper;
using Haelya.Application.DTOs.Category;
using Haelya.Application.DTOs.User;
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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> CreateAsync(CategoryCreateDTO dto)
        {
            if (await _categoryRepository.ExistsByNameAsync(dto.Name))
            {
                throw new CategoryNameAlreadyUsedException();
            }
            Category category = _mapper.Map<Category>(dto);


            await _categoryRepository.AddAsync(category);
            

            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task DisableAsync(int id)
        {
            await _categoryRepository.DisableAsync(id);
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            IEnumerable<Category> categories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO?> GetByIdAsync(int id)
        {
            Category? category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new NotFoundException("Catégorie not found");
            }

            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task UpdateAsync(int id, CategoryUpdateDTO dto)
        {
            Category? existingCategory = await _categoryRepository.GetByIdAsync(id);

            if (existingCategory == null)
            {
                throw new NotFoundException("Catégorie not found");
            }

            _mapper.Map(dto, existingCategory);

            await _categoryRepository.UpdateAsync(existingCategory);
        }
    }
}
