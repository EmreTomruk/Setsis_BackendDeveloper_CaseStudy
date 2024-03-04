using AutoMapper;
using MediatR;
using Setsis.Core.Dtos;
using Setsis.Core.UnitOfWork;
using Setsis.Infrastructure.CQRS.Queries.Product.Request;
using Setsis.Infrastructure.CQRS.Queries.Product.Response;

namespace Setsis.Infrastructure.CQRS.Handlers.Product.QueryHandler
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQueryRequest, Response<GetProductQueryResponse>>
    {
        private readonly IUnitOfWork<SetsisDbContext> _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IUnitOfWork<SetsisDbContext> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<GetProductQueryResponse>> Handle(GetProductQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.GetRepository<Core.Models.Product>().GetByIdAsync(request.Id);

            if (product == null)
                return Response<GetProductQueryResponse>.Fail(new ErrorDto("Product not found"), 404);

            return Response<GetProductQueryResponse>.Success(_mapper.Map<GetProductQueryResponse>(product), 200);
        }
    }
}
