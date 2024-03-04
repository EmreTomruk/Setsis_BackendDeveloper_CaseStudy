using MediatR;
using Setsis.Core.Dtos;
using Setsis.Infrastructure.CQRS.Commands.Products.Response;

namespace Setsis.Infrastructure.CQRS.Commands.Products.Request
{
    public class UpdateProductCommandRequest : IRequest<Response<UpdateProductCommandResponse>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
