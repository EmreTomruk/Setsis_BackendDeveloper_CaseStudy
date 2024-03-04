using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Setsis.Core.Dtos;
using Setsis.Core.UnitOfWork;
using Setsis.Infrastructure.CQRS.Commands.Categories.Request;
using Setsis.Infrastructure.CQRS.Commands.Categories.Response;

namespace Setsis.Infrastructure.CQRS.Handlers.Category.CommandHandler
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest, Response<UpdateCategoryCommandResponse>>
    {
        private readonly IUnitOfWork<SetsisDbContext> _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(IUnitOfWork<SetsisDbContext> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<UpdateCategoryCommandResponse>> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.GetRepository<Core.Models.Category>().Entities.Where(p => p.Id == request.Id).SingleOrDefaultAsync();

            if (category == null)
                return Response<UpdateCategoryCommandResponse>.Fail(new ErrorDto("Category not found"), 404);

            var mapCategoryRequest = _mapper.Map<Core.Models.Category>(request);

            _unitOfWork.GetRepository<Core.Models.Category>().Update(mapCategoryRequest);
            await _unitOfWork.CommmitAsync();

            return Response<UpdateCategoryCommandResponse>.Success(new UpdateCategoryCommandResponse { Id = category.Id }, 204);
        }
    }
}
