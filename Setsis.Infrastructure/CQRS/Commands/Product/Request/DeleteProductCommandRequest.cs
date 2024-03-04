using MediatR;
using Setsis.Core.Dtos;
using Setsis.Infrastructure.CQRS.Commands.Products.Response;

namespace Setsis.Infrastructure.CQRS.Commands.Products.Request
{
    public class DeleteProductCommandRequest : IRequest<Response<DeleteProductCommandResponse>>
    {
        public int Id { get; set; }
    }
}
