using MediatR;
using Setsis.Core.Dtos;
using Setsis.Infrastructure.CQRS.Queries.Product.Response;

namespace Setsis.Infrastructure.CQRS.Queries.Product.Request
{
    public class GetProductQueryRequest : IRequest<Response<GetProductQueryResponse>>
    {
        public int Id { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
