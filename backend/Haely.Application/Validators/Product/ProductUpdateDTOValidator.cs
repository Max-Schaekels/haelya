using FluentValidation;
using Haelya.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Application.Validators.Product
{
    public class ProductUpdateDTOValidator : AbstractValidator<ProductUpdateDTO>
    {
        public ProductUpdateDTOValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Le nom du produit est requis")
                .MaximumLength(255);

            RuleFor(p => p.ImageUrl)
                .NotEmpty().WithMessage("Une image est requise pour le produit")
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _)).WithMessage("L'URL de l'image n'est pas valide")
                .MaximumLength(1024);

            RuleFor(p => p.Description)
                .MaximumLength(1024);

            RuleFor(p => p.SupplierPrice)
                .GreaterThanOrEqualTo(0);


            RuleFor(p => p.Stock)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.CategoryId)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.BrandId)
                .GreaterThan(0).WithMessage("La marque est requise.");
        }
    }
}
