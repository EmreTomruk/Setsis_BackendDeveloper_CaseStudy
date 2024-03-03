using MediatR;

using Setsis.Core.Dtos;
using Setsis.Infrastructure.CQRS.Commands.Response;

namespace Setsis.Infrastructure.CQRS.Commands.Request
{
    public class DeleteCategoryCommandRequest : IRequest<Response<DeleteCategoryCommandResponse>>
    {
        public int Id { get; set; }
    }
}
