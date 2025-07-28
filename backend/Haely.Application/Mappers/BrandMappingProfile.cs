using AutoMapper;
using Haelya.Application.DTOs.Brand;
using Haelya.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Application.Mappers
{
    public class BrandMappingProfile : Profile
    {
        public BrandMappingProfile()
        {
            CreateMap<Brand, BrandDTO>();


            CreateMap<BrandCreateDTO, Brand>();

            CreateMap<BrandUpdateDTO, Brand>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
