using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Shared.Helpers
{
    public static class ProductPricingHelper
    {
        public static decimal CalculateMargin(decimal supplierPrice, decimal price)
        {
            return supplierPrice == 0
                ? 0
                : Math.Round(((price - supplierPrice) / supplierPrice) * 100, 2);
        }

        public static decimal CalculatePrice(decimal supplierPrice, decimal margin)
        {
            return Math.Round(supplierPrice + (supplierPrice * margin / 100), 2);
        }
    }
}
