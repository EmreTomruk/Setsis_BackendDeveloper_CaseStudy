using Setsis.Core.Dtos;
using Setsis.Infrastructure.CQRS.Commands.Categories.Request;
using Setsis.Infrastructure.CQRS.Commands.Categories.Response;
using Setsis.Infrastructure.CQRS.Queries.Category.Response;

namespace Setsis.Service.Services.Category
{
    public interface ICategoryService
    {
        Task<Response<List<GetCategoryQueryResponse>>> GetAllCategoriesAsync();
        Task<Response<GetCategoryQueryResponse>> GetCategoryAsync(int id);
        Task<Response<CreateCategoryCommandResponse>> AddCategoryAsync(CreateCategoryCommandRequest categoryDto);
        Task<Response<UpdateCategoryCommandResponse>> UpdateCategoryAsync(UpdateCategoryCommandRequest categoryDto);
        Task<Response<DeleteCategoryCommandResponse>> DeleteCategoryAsync(int id);
    }
}
