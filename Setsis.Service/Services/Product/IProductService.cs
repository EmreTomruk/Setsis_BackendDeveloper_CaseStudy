using Setsis.Core.Dtos;
using Setsis.Infrastructure.CQRS.Commands.Products.Request;
using Setsis.Infrastructure.CQRS.Commands.Products.Response;
using Setsis.Infrastructure.CQRS.Queries.Product.Response;

namespace Setsis.Service.Services.Product
{
    public interface IProductService
    {
        Task<Response<List<GetProductQueryResponse>>> GetAllProductsAsync();
        Task<Response<GetProductQueryResponse>> GetProductAsync(int id);
        Task<Response<CreateProductCommandResponse>> AddProductAsync(CreateProductCommandRequest productDto);
        Task<Response<UpdateProductCommandResponse>> UpdateProductAsync(UpdateProductCommandRequest productDto);
        Task<Response<DeleteProductCommandResponse>> DeleteProductAsync(int id);
    }
}
