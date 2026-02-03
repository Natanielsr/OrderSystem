using System;
using FluentValidation;
using OrderSystem.Application.Users.Commands.CreateUser;

namespace OrderSystem.Application.Validator;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(u => u.Username)
            .NotNull().WithMessage("username can´t be null")
            .NotEmpty().WithMessage("username can´t be empty");

        RuleFor(u => u.Email)
            .NotNull().WithMessage("email can´t be null")
            .NotEmpty().WithMessage("email can´t be empty")
            .EmailAddress().WithMessage("Invalid email");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one number.")
            .Matches(@"[\!\?\*\.\@\#\$\%\^\-]").WithMessage("Password must contain at least one special character (!?*.@#$%^-).");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("Passwords do not match.");

    }
}
