using FluentValidation;
using Setsis.Infrastructure.CQRS.Commands.Products.Request;

namespace Setsis.Service.Services.Product.Validator
{
    internal class GetProductValidator : AbstractValidator<int>
    {
        public GetProductValidator()
        {
            RuleFor(p => p).NotEqual(0).NotEmpty().WithMessage(p => "Id is required");
        }
    }

    internal class AddProductValidator : AbstractValidator<CreateProductCommandRequest>
    {
        public AddProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage(p => "Name is required");
            RuleFor(p => p.Stock).NotEqual(0).NotEmpty().WithMessage(p => "Stock is required");
            RuleFor(p => p.Price).NotEqual(0).NotEmpty().WithMessage(p => "Price is required");
            RuleFor(p => p.CategoryId).NotEqual(0).NotEmpty().WithMessage(p => "Category is required");
        }
    }

    internal class UpdateProductValidator : AbstractValidator<UpdateProductCommandRequest>
    {
        public UpdateProductValidator()
        {
            RuleFor(p => p.Id).NotEqual(0).NotEmpty().WithMessage(p => "Id is required");
            RuleFor(p => p.Name).NotEmpty().WithMessage(p => "Name is required");
            RuleFor(p => p.Stock).NotEqual(0).NotEmpty().WithMessage(p => "Stock is required");
            RuleFor(p => p.Price).NotEqual(0).NotEmpty().WithMessage(p => "Price is required");
            RuleFor(p => p.CategoryId).NotEqual(0).NotEmpty().WithMessage(p => "Category is required");
        }
    }
}
