using Setsis.Infrastructure.CQRS.Commands;

namespace Setsis.Infrastructure.CQRS.Queries.Product.Response
{
    public class GetProductQueryResponse : BaseCommandResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
