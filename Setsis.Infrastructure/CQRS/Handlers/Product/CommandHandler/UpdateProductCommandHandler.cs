using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Setsis.Core.Dtos;
using Setsis.Core.UnitOfWork;
using Setsis.Infrastructure.CQRS.Commands.Products.Request;
using Setsis.Infrastructure.CQRS.Commands.Products.Response;

namespace Setsis.Infrastructure.CQRS.Handlers.Product.CommandHandler
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, Response<UpdateProductCommandResponse>>
    {
        private readonly IUnitOfWork<SetsisDbContext> _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IUnitOfWork<SetsisDbContext> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<UpdateProductCommandResponse>> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.GetRepository<Core.Models.Product>().Entities.Where(p => p.Id == request.Id).AsNoTracking().SingleOrDefaultAsync();

            if (product == null)
                return Response<UpdateProductCommandResponse>.Fail(new ErrorDto("Product not found"), 404);

            var mapProductRequest = _mapper.Map<Core.Models.Product>(request);

            _unitOfWork.GetRepository<Core.Models.Product>().Update(mapProductRequest);
            await _unitOfWork.CommmitAsync();

            return Response<UpdateProductCommandResponse>.Success(new UpdateProductCommandResponse { Id = mapProductRequest.Id }, 204);
        }
    }
}
