using MediatR;
using Setsis.Core.Dtos;
using Setsis.Infrastructure.CQRS.Commands.Categories.Response;

namespace Setsis.Infrastructure.CQRS.Commands.Categories.Request
{
    public class DeleteCategoryCommandRequest : IRequest<Response<DeleteCategoryCommandResponse>>
    {
        public int Id { get; set; }
    }
}
