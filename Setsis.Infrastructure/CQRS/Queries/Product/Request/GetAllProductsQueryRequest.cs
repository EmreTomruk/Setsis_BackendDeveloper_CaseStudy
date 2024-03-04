using MediatR;
using Setsis.Core.Dtos;
using Setsis.Infrastructure.CQRS.Queries.Product.Response;

namespace Setsis.Infrastructure.CQRS.Queries.Product.Request
{
    public class GetAllProductsQueryRequest : IRequest<Response<List<GetProductQueryResponse>>> { }
}
