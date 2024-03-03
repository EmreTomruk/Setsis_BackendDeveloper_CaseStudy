using MediatR;

using Setsis.Core.Dtos;
using Setsis.Infrastructure.CQRS.Queries.Response;

namespace Setsis.Infrastructure.CQRS.Queries.Request
{
    public class GetCategoryQueryRequest : IRequest<Response<GetCategoryQueryResponse>>
    {
        public int Id { get; set; }
    }
}
