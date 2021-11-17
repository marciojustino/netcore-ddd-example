using DDDExample.Domain.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDExample.Service.Validators
{
    internal class AuthValidator : AbstractValidator<AuthDto>
    {
        public AuthValidator()
        {
            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("Please enter the email.")
                .NotNull().WithMessage("Please enter the email.");

            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Please enter the password.")
                .NotNull().WithMessage("Please enter the password.")
                .MinimumLength(8).WithMessage("Please enter at least 8 caracters to password.");
        }
    }
}
