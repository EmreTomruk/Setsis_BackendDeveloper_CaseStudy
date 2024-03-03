using MediatR;
using Microsoft.EntityFrameworkCore;
using Setsis.Core.Dtos;
using Setsis.Core.UnitOfWork;
using Setsis.Infrastructure.CQRS.Commands.Categories.Request;
using Setsis.Infrastructure.CQRS.Commands.Categories.Response;

namespace Setsis.Infrastructure.CQRS.Handlers.Category.CommandHandler
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommandRequest, Response<DeleteCategoryCommandResponse>>
    {
        private readonly IUnitOfWork<SetsisDbContext> _unitOfWork;

        public DeleteCategoryCommandHandler(IUnitOfWork<SetsisDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<DeleteCategoryCommandResponse>> Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.GetRepository<Core.Models.Category>().Entities.Where(p => p.Id == request.Id).SingleOrDefaultAsync();

            if (category == null)
                return Response<DeleteCategoryCommandResponse>.Fail(new ErrorDto("Category not found"), 404);

            _unitOfWork.GetRepository<Core.Models.Category>().Remove(category);
            await _unitOfWork.CommmitAsync();

            return Response<DeleteCategoryCommandResponse>.Success(new DeleteCategoryCommandResponse { Id = category.Id }, 204);
        }
    }
}
