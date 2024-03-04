using FluentValidation;
using Setsis.Infrastructure.CQRS.Commands.Categories.Request;

namespace Setsis.Service.Services.Category.Validator
{
    internal class GetCategoryValidator : AbstractValidator<int>
    {
        public GetCategoryValidator()
        {
            RuleFor(p => p).NotEqual(0).NotEmpty().WithMessage(p => "Id is required");
        }
    }

    internal class AddCategoryValidator : AbstractValidator<CreateCategoryCommandRequest>
    {
        public AddCategoryValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage(p => "Name is required");
        }
    }

    internal class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommandRequest>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(p => p.Id).NotEqual(0).NotEmpty().WithMessage(p => "Id is required");

            RuleFor(p => p.Name).NotEmpty().WithMessage(p => "Name is required");
        }
    }
}
