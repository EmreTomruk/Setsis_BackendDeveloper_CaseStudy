using FluentValidation;
using Setsis.Service.Services.User.Dto.Request;

namespace Setsis.Service.Services.User.Validator
{
    internal class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserValidator()
        {
            RuleFor(p => p.Username).NotNull().NotEmpty().WithMessage(p => "Username is required");

            RuleFor(p => p.Email).NotNull().NotEmpty().WithMessage(p => "Email is required");

            RuleFor(p => p.Password).NotNull().NotEmpty().WithMessage(p => "Password is required");
        }
    }
}
