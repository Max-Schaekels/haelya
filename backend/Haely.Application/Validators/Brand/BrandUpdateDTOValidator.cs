using FluentValidation;
using Haelya.Application.DTOs.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Application.Validators.Brand
{
    public class BrandUpdateDTOValidator : AbstractValidator<BrandUpdateDTO>
    {
        public BrandUpdateDTOValidator()
        {
            RuleFor(b => b.Id)
                .GreaterThan(0);

            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("Le nom de la marque est requis.")
                .MaximumLength(255);
        }
    }
}
