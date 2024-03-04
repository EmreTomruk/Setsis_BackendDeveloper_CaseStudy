using AutoMapper;
using MediatR;
using Setsis.Core.Dtos;
using Setsis.Core.UnitOfWork;
using Setsis.Infrastructure.CQRS.Commands.Products.Request;
using Setsis.Infrastructure.CQRS.Commands.Products.Response;

namespace Setsis.Infrastructure.CQRS.Handlers.Product.CommandHandler
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, Response<CreateProductCommandResponse>>
    {
        private readonly IUnitOfWork<SetsisDbContext> _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IUnitOfWork<SetsisDbContext> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<CreateProductCommandResponse>> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var newProduct = _mapper.Map<Core.Models.Product>(request);

            await _unitOfWork.GetRepository<Core.Models.Product>().AddAsync(newProduct);
            await _unitOfWork.CommmitAsync();

            return Response<CreateProductCommandResponse>.Success(new CreateProductCommandResponse { Id = newProduct.Id }, 201);
        }
    }
}
