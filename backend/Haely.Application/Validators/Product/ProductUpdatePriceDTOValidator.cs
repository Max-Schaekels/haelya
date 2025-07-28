using FluentValidation;
using Haelya.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Application.Validators.Product
{
    public class ProductUpdatePriceDTOValidator : AbstractValidator<ProductUpdatePriceDTO>
    {
        public ProductUpdatePriceDTOValidator()
        {
            RuleFor(p => p.Price)
                .GreaterThan(0)
                .WithMessage("Le prix de vente doit être supérieur à 0.");
        }
    }
}
