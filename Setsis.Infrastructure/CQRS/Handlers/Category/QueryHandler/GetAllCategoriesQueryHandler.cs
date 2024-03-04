using AutoMapper;
using MediatR;
using Setsis.Core.Dtos;
using Setsis.Core.UnitOfWork;
using Setsis.Infrastructure.CQRS.Queries.Category.Request;
using Setsis.Infrastructure.CQRS.Queries.Category.Response;

namespace Setsis.Infrastructure.CQRS.Handlers.Category.QueryHandler
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQueryRequest, Response<List<GetCategoryQueryResponse>>>
    {
        private readonly IUnitOfWork<SetsisDbContext> _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCategoriesQueryHandler(IUnitOfWork<SetsisDbContext> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<List<GetCategoryQueryResponse>>> Handle(GetAllCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.GetRepository<Core.Models.Category>().GetAllAsync();

            if (categories == null)
                return Response<List<GetCategoryQueryResponse>>.Fail(new ErrorDto("Category not found"), 404);

            return Response<List<GetCategoryQueryResponse>>.Success(_mapper.Map<List<GetCategoryQueryResponse>>(categories), 200);
        }
    }
}
