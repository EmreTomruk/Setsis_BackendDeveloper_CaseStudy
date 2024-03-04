using MediatR;
using Setsis.Core.Dtos;
using Setsis.Core.Validation;
using Setsis.Service.Services.Product.Validator;
using Setsis.Infrastructure.CQRS.Commands.Products.Response;
using Setsis.Infrastructure.CQRS.Commands.Products.Request;
using Setsis.Service.Services.Product;
using Setsis.Infrastructure.CQRS.Queries.Product.Response;
using Setsis.Infrastructure.CQRS.Queries.Product.Request;

namespace Setsis.Service.Product
{
    public class ProductService : IProductService
    {
        private readonly IValidationService _validationService;
        private readonly IMediator _mediator;

        public ProductService(IValidationService validationService, IMediator mediator) 
        { 
            _mediator = mediator;
            _validationService = validationService;
        }

        public async Task<Response<List<GetProductQueryResponse>>> GetAllProductsAsync()
        {
            return await _mediator.Send(new GetAllProductsQueryRequest());
        }

        public async Task<Response<GetProductQueryResponse>> GetProductAsync(int id)
        {
            var validationResult = _validationService.Validate(typeof(GetProductValidator), id);

            if (!validationResult.IsValid)
                return Response<GetProductQueryResponse>.Fail(new ErrorDto(validationResult.ErrorMessages), 400);

            return await _mediator.Send(new GetProductQueryRequest { Id = id });
        }


        public async Task<Response<CreateProductCommandResponse>> AddProductAsync(CreateProductCommandRequest productDto)
        {
            var validationResult = _validationService.Validate(typeof(AddProductValidator), productDto);

            if (!validationResult.IsValid)
                return Response<CreateProductCommandResponse>.Fail(new ErrorDto(validationResult.ErrorMessages), 400);

            return await _mediator.Send(productDto);
        }

        public async Task<Response<UpdateProductCommandResponse>> UpdateProductAsync(UpdateProductCommandRequest productDto)
        {
            var validationResult = _validationService.Validate(typeof(UpdateProductValidator), 
                productDto);

            if (!validationResult.IsValid)
                return Response<UpdateProductCommandResponse>.Fail(new ErrorDto(validationResult.ErrorMessages), 400);

            return await _mediator.Send(productDto);
        }

        public async Task<Response<DeleteProductCommandResponse>> DeleteProductAsync(int id)
        {
            var validationResult = _validationService.Validate(typeof(GetProductValidator), id);

            if (!validationResult.IsValid)
                return Response<DeleteProductCommandResponse>.Fail(new ErrorDto(validationResult.ErrorMessages), 400);

            return await _mediator.Send(new DeleteProductCommandRequest { Id = id });
        }
    }
}
