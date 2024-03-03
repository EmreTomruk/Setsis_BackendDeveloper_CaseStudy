using FluentValidation;
using Setsis.Service.Services.Category.Dto;

namespace Setsis.Service.Services.Category.Validator
{
    internal class GetCategoryValidator : AbstractValidator<int>
    {
        public GetCategoryValidator()
        {
            RuleFor(p => p).NotEqual(0).NotEmpty().WithMessage(p => "Id is required");
        }
    }

    internal class AddCategoryValidator : AbstractValidator<AddCategoryDto>
    {
        public AddCategoryValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage(p => "Name is required");
        }
    }

    internal class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(p => p.Id).NotEqual(0).NotEmpty().WithMessage(p => "Id is required");

            RuleFor(p => p.Name).NotEmpty().WithMessage(p => "Name is required");
        }
    }
}
