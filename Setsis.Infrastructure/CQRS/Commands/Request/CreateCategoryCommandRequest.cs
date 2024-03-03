using MediatR;

using Setsis.Core.Dtos;
using Setsis.Infrastructure.CQRS.Commands.Response;

namespace Setsis.Infrastructure.CQRS.Commands.Request
{
    public class CreateCategoryCommandRequest : IRequest<Response<CreateCategoryCommandResponse>>
    {
        public string Name { get; set; }
    }
}
