using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeploAPI.Models.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName).NotNull().WithMessage(x => "FirstName является обязательным").NotEmpty().WithMessage(x => "FirstName является обязательным");
            RuleFor(x => x.LastName).NotNull().WithMessage(x => "LastName является обязательным").NotEmpty().WithMessage(x => "LastName является обязательным");
            RuleFor(x => x.Email).NotNull().WithMessage(x => "Email является обязательным").NotEmpty().WithMessage(x => "Email является обязательным");
            RuleFor(x => x.Email)
                .NotNull().WithMessage(x => "Email является обязательным")
                .NotEmpty().WithMessage(x => "Email является обязательным")
                .EmailAddress().WithMessage(x => "Некорректный Email");
            RuleFor(x => x.Password).NotNull().WithMessage(x => "Password является обязательным").NotEmpty().WithMessage(x => "Password является обязательным");
        }
    }
}
