using FluentValidation;
using Haelya.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Application.Validators.User
{
    public class LoginDTOValidator : AbstractValidator<LoginDTO>
    {
        public LoginDTOValidator() 
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("L'adresse email est requise.")
                .EmailAddress().WithMessage("L'adresse email n'est pas valide.");

            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Le mot de passe est requis.")
            .MinimumLength(8).WithMessage("Le mot de passe doit contenir au moins 8 caractères.")
            .Matches(@"[A-Z]").WithMessage("Le mot de passe doit contenir au moins une majuscule.")
            .Matches(@"[a-z]").WithMessage("Le mot de passe doit contenir au moins une minuscule.")
            .Matches(@"\d").WithMessage("Le mot de passe doit contenir au moins un chiffre.")
            .Matches(@"[\!\?\*\.@#\$%\^&\-_]").WithMessage("Le mot de passe doit contenir au moins un caractère spécial.");
        }
    }
}
