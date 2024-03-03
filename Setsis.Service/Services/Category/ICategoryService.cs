using Setsis.Core.Dtos;
using Setsis.Infrastructure.CQRS.Commands.Response;
using Setsis.Infrastructure.CQRS.Queries.Response;
using Setsis.Service.Services.Category.Dto;

namespace Setsis.Service.Services.Category
{
    public interface ICategoryService
    {
        Task<Response<GetCategoryQueryResponse>> GetCategoryAsync(int id);
        Task<Response<CreateCategoryCommandResponse>> AddCategoryAsync(AddCategoryDto categoryDto);
        Task<Response<UpdateCategoryCommandResponse>> UpdateCategoryAsync(int id, UpdateCategoryDto categoryDto);
        Task<Response<DeleteCategoryCommandResponse>> DeleteCategoryAsync(int id);
    }
}
