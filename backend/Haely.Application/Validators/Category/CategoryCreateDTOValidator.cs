using FluentValidation;
using Haelya.Application.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Application.Validators.Category
{
    public class CategoryCreateDTOValidator : AbstractValidator<CategoryCreateDTO>
    {
        public CategoryCreateDTOValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Le nom de la catégorie est requis.")
                .MaximumLength(255);

            RuleFor(c => c.Description)
                .MaximumLength(1024);
        }
    }
}
