using MediatR;
using Microsoft.EntityFrameworkCore;
using Setsis.Core.Dtos;
using Setsis.Core.UnitOfWork;
using Setsis.Infrastructure.CQRS.Commands.Products.Request;
using Setsis.Infrastructure.CQRS.Commands.Products.Response;

namespace Setsis.Infrastructure.CQRS.Handlers.Product.CommandHandler
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, Response<DeleteProductCommandResponse>>
    {
        private readonly IUnitOfWork<SetsisDbContext> _unitOfWork;

        public DeleteProductCommandHandler(IUnitOfWork<SetsisDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<DeleteProductCommandResponse>> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.GetRepository<Core.Models.Product>().Entities.Where(p => p.Id == request.Id).SingleOrDefaultAsync();

            if (product == null)
                return Response<DeleteProductCommandResponse>.Fail(new ErrorDto("Product not found"), 404);

            _unitOfWork.GetRepository<Core.Models.Product>().Remove(product);
            await _unitOfWork.CommmitAsync();

            return Response<DeleteProductCommandResponse>.Success(new DeleteProductCommandResponse { Id = product.Id }, 204);
        }
    }
}
