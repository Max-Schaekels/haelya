using Haelya.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Domain.Filters
{
    public class ProductFilterPublic : PaginationQuery
    {
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? Search { get; set; }


        // Tri
        public string? SortBy { get; set; }
        public string? SortDirection { get; set; }
    }
}
