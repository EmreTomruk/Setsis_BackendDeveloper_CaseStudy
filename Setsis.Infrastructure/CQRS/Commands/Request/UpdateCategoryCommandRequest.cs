using MediatR;

using Setsis.Core.Dtos;
using Setsis.Infrastructure.CQRS.Commands.Response;

namespace Setsis.Infrastructure.CQRS.Commands.Request
{
    public class UpdateCategoryCommandRequest : IRequest<Response<UpdateCategoryCommandResponse>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
