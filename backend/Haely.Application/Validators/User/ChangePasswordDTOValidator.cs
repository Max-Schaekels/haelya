using FluentValidation;
using Haelya.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Application.Validators.User
{
    public class ChangePasswordDTOValidator : AbstractValidator<ChangePasswordDTO>
    {
        public ChangePasswordDTOValidator() 
        {
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Le mot de passe est requis.")
                .MinimumLength(8).WithMessage("Le mot de passe doit contenir au moins 8 caractères.")
                .Matches(@"[A-Z]").WithMessage("Le mot de passe doit contenir au moins une majuscule.")
                .Matches(@"[a-z]").WithMessage("Le mot de passe doit contenir au moins une minuscule.")
                .Matches(@"\d").WithMessage("Le mot de passe doit contenir au moins un chiffre.")
                .Matches(@"[\!\?\*\.@#\$%\^&\-_]").WithMessage("Le mot de passe doit contenir au moins un caractère spécial.");

            RuleFor(x => x.OldPassword)
                .NotEmpty().WithMessage("L'ancien mot de passe est requis.");

            RuleFor(x => x)
                .Must(dto => dto.OldPassword != dto.NewPassword)
                .WithMessage("Le nouveau mot de passe doit être différent de l'ancien.");
        }
    }
}
