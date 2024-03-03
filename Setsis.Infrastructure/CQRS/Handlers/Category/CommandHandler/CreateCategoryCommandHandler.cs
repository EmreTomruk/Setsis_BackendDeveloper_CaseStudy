using MediatR;
using Setsis.Core.Dtos;
using Setsis.Core.UnitOfWork;
using Setsis.Infrastructure.CQRS.Commands.Categories.Request;
using Setsis.Infrastructure.CQRS.Commands.Categories.Response;

namespace Setsis.Infrastructure.CQRS.Handlers.Category.CommandHandler
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommandRequest, Response<CreateCategoryCommandResponse>>
    {
        private readonly IUnitOfWork<SetsisDbContext> _unitOfWork;

        public CreateCategoryCommandHandler(IUnitOfWork<SetsisDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<CreateCategoryCommandResponse>> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category = new Core.Models.Category { Name = request.Name };

            await _unitOfWork.GetRepository<Core.Models.Category>().AddAsync(category);
            await _unitOfWork.CommmitAsync();

            return Response<CreateCategoryCommandResponse>.Success(new CreateCategoryCommandResponse { Id = category.Id }, 201);
        }
    }
}
