using FluentValidation;
using Setsis.Service.Services.Authenticate.Dto.Request;

namespace Setsis.Service.Services.Authenticate.Validator
{
    internal class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(p => p.Email).NotNull().NotEmpty().WithMessage(p => "Email is required");

            RuleFor(p => p.Password).NotNull().NotEmpty().WithMessage(p => "Password is required");
        }
    }
}
