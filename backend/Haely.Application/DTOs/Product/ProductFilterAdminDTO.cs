using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Application.DTOs.Product
{
    public class ProductFilterAdminDTO : ProductFilterPublicDTO
    {
        public bool? IsActive { get; set; }
    }
}
