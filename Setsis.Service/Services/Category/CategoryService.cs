using MediatR;
using Setsis.Service.Services.Category;
using Setsis.Core.Dtos;
using Setsis.Service.Services.Category.Dto;
using Setsis.Core.Validation;
using Setsis.Service.Services.Category.Validator;
using Setsis.Infrastructure.CQRS.Commands.Categories.Response;
using Setsis.Infrastructure.CQRS.Queries.Category.Response;
using Setsis.Infrastructure.CQRS.Queries.Category.Request;
using Setsis.Infrastructure.CQRS.Commands.Categories.Request;

namespace Setsis.Service.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly IValidationService _validationService;
        private readonly IMediator _mediator;

        public CategoryService(IValidationService validationService, IMediator mediator) 
        { 
            _mediator = mediator;
            _validationService = validationService;
        }

        public async Task<Response<GetCategoryQueryResponse>> GetCategoryAsync(int id)
        {
            var validationResult = _validationService.Validate(typeof(GetCategoryValidator), id);

            if (!validationResult.IsValid)
                return Response<GetCategoryQueryResponse>.Fail(new ErrorDto(validationResult.ErrorMessages), 400);

            return await _mediator.Send(new GetCategoryQueryRequest { Id = id });
        }


        public async Task<Response<CreateCategoryCommandResponse>> AddCategoryAsync(AddCategoryDto categoryDto)
        {
            var validationResult = _validationService.Validate(typeof(AddCategoryValidator), categoryDto);

            if (!validationResult.IsValid)
                return Response<CreateCategoryCommandResponse>.Fail(new ErrorDto(validationResult.ErrorMessages), 400);

            return await _mediator.Send(new CreateCategoryCommandRequest { Name = categoryDto.Name });
        }

        public async Task<Response<UpdateCategoryCommandResponse>> UpdateCategoryAsync(int id, UpdateCategoryDto categoryDto)
        {
            var validationResult = _validationService.Validate(typeof(UpdateCategoryValidator), 
                new UpdateCategoryDto { Id = id, Name = categoryDto.Name });

            if (!validationResult.IsValid)
                return Response<UpdateCategoryCommandResponse>.Fail(new ErrorDto(validationResult.ErrorMessages), 400);

            return await _mediator.Send(new UpdateCategoryCommandRequest { Name = categoryDto.Name });
        }

        public async Task<Response<DeleteCategoryCommandResponse>> DeleteCategoryAsync(int id)
        {
            var validationResult = _validationService.Validate(typeof(GetCategoryValidator), id);

            if (!validationResult.IsValid)
                return Response<DeleteCategoryCommandResponse>.Fail(new ErrorDto(validationResult.ErrorMessages), 400);

            return await _mediator.Send(new DeleteCategoryCommandRequest { Id = id });
        }
    }
}
