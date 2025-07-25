using AutoMapper;
using Haelya.Application.DTOs.Category;
using Haelya.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Application.Mappers
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryDTO>();

            CreateMap< CategoryCreateDTO, Category>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));

            CreateMap<CategoryUpdateDTO, Category>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
