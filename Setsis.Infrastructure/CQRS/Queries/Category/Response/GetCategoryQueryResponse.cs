using Setsis.Infrastructure.CQRS.Commands;

namespace Setsis.Infrastructure.CQRS.Queries.Category.Response
{
    public class GetCategoryQueryResponse : BaseCommandResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
