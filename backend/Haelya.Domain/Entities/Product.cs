using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal SupplierPrice { get; set; }
        public int Stock {  get; set; }
        public string Categorie { get; set; }
        public decimal Margin { get; set; } = 15;
        public int ViewCount { get; set; } = 0;
        public decimal AvgNote { get; set; } =0 ;
        public bool InSlide { get; set; } = false ;
        public bool IsActive { get; set; } = false;
        public bool Featured { get; set; }=false ;

    }
}
