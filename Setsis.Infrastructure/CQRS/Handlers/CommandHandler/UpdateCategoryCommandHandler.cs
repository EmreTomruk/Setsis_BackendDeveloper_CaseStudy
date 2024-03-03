﻿using MediatR;
using Microsoft.EntityFrameworkCore;

using Setsis.Core.Dtos;
using Setsis.Core.Models;
using Setsis.Core.UnitOfWork;
using Setsis.Infrastructure.CQRS.Commands.Request;
using Setsis.Infrastructure.CQRS.Commands.Response;

namespace Setsis.Infrastructure.CQRS.Handlers.CommandHandler
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest, Response<UpdateCategoryCommandResponse>>
    {
        private readonly IUnitOfWork<SetsisDbContext> _unitOfWork;

        public UpdateCategoryCommandHandler(IUnitOfWork<SetsisDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;            
        }

        public async Task<Response<UpdateCategoryCommandResponse>> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.GetRepository<Category>().Entities.Where(p => p.Id == request.Id).SingleOrDefaultAsync();

            if (category == null)
                return Response<UpdateCategoryCommandResponse>.Fail(new ErrorDto("Category not found"), 404);

            category.Name = request.Name;

            _unitOfWork.GetRepository<Category>().Update(category);
            await _unitOfWork.CommmitAsync();

            return Response<UpdateCategoryCommandResponse>.Success(new UpdateCategoryCommandResponse { Id = category.Id }, 204);
        }
    }
}
