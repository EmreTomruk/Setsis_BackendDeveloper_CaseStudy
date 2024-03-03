using Setsis.Infrastructure.CQRS.Commands.Response;

namespace Setsis.Infrastructure.CQRS.Queries.Response
{
    public class GetCategoryQueryResponse : BaseCommandResponse
    {
        public string Name { get; set; }
    }
}
