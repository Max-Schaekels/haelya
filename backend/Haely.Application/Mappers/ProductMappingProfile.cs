using AutoMapper;
using Haelya.Application.DTOs.Product;
using Haelya.Domain.Entities;
using Haelya.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Application.Mappers
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            // Product => ProductDTO
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            // ProductCreateDTO => Product
            CreateMap<ProductCreateDTO, Product>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => ProductPricingHelper.CalculatePrice(src.SupplierPrice, src.Margin)))
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.InSlide, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.Featured, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.ViewCount, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.NoteCount, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.TotalNote, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false));

            // ProductUpdateDTO => Product
            CreateMap<ProductUpdateDTO, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Price, opt => opt.Ignore())
                .ForMember(dest => dest.Margin, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.InSlide, opt => opt.Ignore())
                .ForMember(dest => dest.Featured, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
                .ForMember(dest => dest.DateDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.ViewCount, opt => opt.Ignore())
                .ForMember(dest => dest.NoteCount, opt => opt.Ignore())
                .ForMember(dest => dest.TotalNote, opt => opt.Ignore())
                .ForMember(dest => dest.DateUpdated, opt => opt.MapFrom(_ => DateTime.UtcNow));
        }
    }
}
