using AutoMapper;
using Haelya.Application.DTOs.Common;
using Haelya.Domain.Common;
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
            CreateMap(typeof(PagedResult<>), typeof(PagedResultDTO<>));
            CreateMap<PaginationQueryDTO, PaginationQuery>();
        }
    }
}

