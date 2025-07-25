﻿using FluentValidation;
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
                .MaximumLength(50)
                .When(x => !string.IsNullOrWhiteSpace(x.FirstName));
            

            RuleFor(x => x.LastName)
                .MaximumLength(50)
                .When(x => !string.IsNullOrWhiteSpace(x.LastName));

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
