using MediatR;

using Setsis.Core.Dtos;
using Setsis.Core.Models;
using Setsis.Core.UnitOfWork;
using Setsis.Infrastructure.CQRS.Commands.Request;
using Setsis.Infrastructure.CQRS.Commands.Response;

namespace Setsis.Infrastructure.CQRS.Handlers.CommandHandler
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
            var category = new Category { Name = request.Name };

            await _unitOfWork.GetRepository<Category>().AddAsync(category);
            await _unitOfWork.CommmitAsync();

            return Response<CreateCategoryCommandResponse>.Success(new CreateCategoryCommandResponse { Id = category.Id }, 201);
        }
    }
}
