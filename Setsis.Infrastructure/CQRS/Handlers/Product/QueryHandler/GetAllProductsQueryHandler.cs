using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Setsis.Core.Dtos;
using Setsis.Core.UnitOfWork;
using Setsis.Infrastructure.CQRS.Queries.Product.Request;
using Setsis.Infrastructure.CQRS.Queries.Product.Response;

namespace Setsis.Infrastructure.CQRS.Handlers.Product.QueryHandler
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, Response<List<GetProductQueryResponse>>>
    {
        private readonly IUnitOfWork<SetsisDbContext> _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IUnitOfWork<SetsisDbContext> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<List<GetProductQueryResponse>>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.GetRepository<Core.Models.Product>().Entities
                    .Include(i => i.Category)
                .OrderBy(i => i.Category)
                .AsNoTracking().ToListAsync();

            if (products == null)
                return Response<List<GetProductQueryResponse>>.Fail(new ErrorDto("Product not found"), 404);

            return Response<List<GetProductQueryResponse>>.Success(_mapper.Map<List<GetProductQueryResponse>>(products), 200);
        }
    }
}
