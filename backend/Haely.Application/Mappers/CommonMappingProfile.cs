using AutoMapper;
using Haelya.Application.DTOs.Common;
using Haelya.Application.DTOs.Product;
using Haelya.Domain.Common;
using Haelya.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Application.Mappers
{
    public class CommonMappingProfile : Profile
    {
        public CommonMappingProfile()
        {
            // Pagination
            CreateMap<PaginationQueryDTO, PaginationQuery>();

            // Product Filters
            CreateMap<ProductFilterPublicDTO, ProductFilterPublic>()
                .IncludeMembers(src => src); // mappe aussi Page, PageSize
            CreateMap<ProductFilterAdminDTO, ProductFilterAdmin>()
                .IncludeMembers(src => src); // mappe tout, y compris isActive

            // Résultats paginés
            CreateMap(typeof(PagedResult<>), typeof(PagedResultDTO<>));
        }
    }
}

