namespace DDDExample.Service.Validators
{
    using Domain.Entities;
    using FluentValidation;

    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Please enter the name.")
                .NotNull().WithMessage("Please enter the name.");

            RuleFor(c => c.Email.Value)
                .NotNull().WithMessage("Please enter a email.")
                .EmailAddress().WithMessage("Please enter a valid email.");

            RuleFor(c => c.CurrentPassword)
                .NotEmpty().WithMessage("Please enter the password.")
                .NotNull().WithMessage("Please enter the password.");
        }
    }
}