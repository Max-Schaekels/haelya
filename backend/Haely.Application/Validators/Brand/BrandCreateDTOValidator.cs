using FluentValidation;
using Haelya.Application.DTOs.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Application.Validators.Brand
{
    public class BrandCreateDTOValidator : AbstractValidator<BrandCreateDTO>
    {
        public BrandCreateDTOValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("Le nom de la marque est requis.")
                .MaximumLength(255);
        }
    }
}
