using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Application.DTOs.Product
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string ImageUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal SupplierPrice { get; set; }
        public decimal Margin { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public int ViewCount { get; set; }
        public decimal AvgNote { get; set; }
        public bool InSlide { get; set; }
        public bool IsActive { get; set; }
        public bool Featured { get; set; }
    }
}
