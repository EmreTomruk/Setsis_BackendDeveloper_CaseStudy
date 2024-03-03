using MediatR;
using Microsoft.EntityFrameworkCore;
using Setsis.Core.Dtos;
using Setsis.Core.UnitOfWork;
using Setsis.Infrastructure.CQRS.Queries.Category.Request;
using Setsis.Infrastructure.CQRS.Queries.Category.Response;

namespace Setsis.Infrastructure.CQRS.Handlers.Category.QueryHandler
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQueryRequest, Response<GetCategoryQueryResponse>>
    {
        private readonly IUnitOfWork<SetsisDbContext> _unitOfWork;

        public GetCategoryQueryHandler(IUnitOfWork<SetsisDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<GetCategoryQueryResponse>> Handle(GetCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.GetRepository<Core.Models.Category>().Entities.Where(p => p.Id == request.Id).SingleOrDefaultAsync();

            if (category == null)
                return Response<GetCategoryQueryResponse>.Fail(new ErrorDto("Category not found"), 404);

            return Response<GetCategoryQueryResponse>.Success(new GetCategoryQueryResponse { Id = category.Id, Name = category.Name }, 200);
        }
    }
}
