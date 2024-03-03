using MediatR;
using Setsis.Core.Dtos;
using Setsis.Infrastructure.CQRS.Commands.Categories.Response;

namespace Setsis.Infrastructure.CQRS.Commands.Categories.Request
{
    public class CreateCategoryCommandRequest : IRequest<Response<CreateCategoryCommandResponse>>
    {
        public string Name { get; set; }
    }
}
