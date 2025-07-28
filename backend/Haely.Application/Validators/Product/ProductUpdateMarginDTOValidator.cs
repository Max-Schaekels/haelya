using FluentValidation;
using Haelya.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Application.Validators.Product
{
    public class ProductUpdateMarginDTOValidator :  AbstractValidator<ProductUpdateMarginDTO>
    {
        public ProductUpdateMarginDTOValidator()
        {
            RuleFor(p => p.Margin)
                .GreaterThanOrEqualTo(0)
                .WithMessage("La marge doit être positive ou nulle.");
        }
    }
}
