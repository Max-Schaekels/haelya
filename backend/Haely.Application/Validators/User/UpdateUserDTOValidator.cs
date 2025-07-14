using FluentValidation;
using Haelya.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Application.Validators.User
{
    public class UpdateUserDTOValidator : AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserDTOValidator() 
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Le prénom est requis.")
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Le nom est requis.")
                .MaximumLength(50);

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+?[0-9]{7,15}$")
                .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber))
                .WithMessage("Le numéro de téléphone est invalide.");

            RuleFor(x => x.BirthDate)
                .LessThan(DateTime.Today).WithMessage("La date de naissance doit être dans le passé.")
                .When(x => x.BirthDate.HasValue);
        }
    }
}
