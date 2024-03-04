using AutoMapper;
using MediatR;
using Setsis.Core.Dtos;
using Setsis.Core.UnitOfWork;
using Setsis.Infrastructure.CQRS.Queries.Category.Request;
using Setsis.Infrastructure.CQRS.Queries.Category.Response;

namespace Setsis.Infrastructure.CQRS.Handlers.Category.QueryHandler
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQueryRequest, Response<GetCategoryQueryResponse>>
    {
        private readonly IUnitOfWork<SetsisDbContext> _unitOfWork;
        private readonly IMapper _mapper;

        public GetCategoryQueryHandler(IUnitOfWork<SetsisDbContext> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<GetCategoryQueryResponse>> Handle(GetCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.GetRepository<Core.Models.Category>().GetByIdAsync(request.Id);

            if (category == null)
                return Response<GetCategoryQueryResponse>.Fail(new ErrorDto("Category not found"), 404);

            return Response<GetCategoryQueryResponse>.Success(_mapper.Map<GetCategoryQueryResponse>(category), 200);
        }
    }
}
