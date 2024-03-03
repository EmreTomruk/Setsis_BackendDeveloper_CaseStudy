using Setsis.Infrastructure.CQRS.Commands.Categories.Response;

namespace Setsis.Infrastructure.CQRS.Queries.Category.Response
{
    public class GetCategoryQueryResponse : BaseCommandResponse
    {
        public string Name { get; set; }
    }
}
