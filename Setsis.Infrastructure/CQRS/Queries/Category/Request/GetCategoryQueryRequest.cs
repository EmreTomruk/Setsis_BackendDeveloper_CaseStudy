using MediatR;
using Setsis.Core.Dtos;
using Setsis.Infrastructure.CQRS.Queries.Category.Response;

namespace Setsis.Infrastructure.CQRS.Queries.Category.Request
{
    public class GetCategoryQueryRequest : IRequest<Response<GetCategoryQueryResponse>>
    {
        public int Id { get; set; }
    }
}
