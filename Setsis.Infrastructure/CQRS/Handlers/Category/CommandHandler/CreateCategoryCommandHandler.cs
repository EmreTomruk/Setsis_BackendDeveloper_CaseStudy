using AutoMapper;
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
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IUnitOfWork<SetsisDbContext> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<CreateCategoryCommandResponse>> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var newCategory = _mapper.Map<Core.Models.Category>(request);

            await _unitOfWork.GetRepository<Core.Models.Category>().AddAsync(newCategory);
            await _unitOfWork.CommmitAsync();

            return Response<CreateCategoryCommandResponse>.Success(new CreateCategoryCommandResponse { Id = newCategory.Id }, 201);
        }
    }
}
