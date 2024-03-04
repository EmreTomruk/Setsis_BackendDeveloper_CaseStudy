using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Setsis.Infrastructure.CQRS.Commands.Categories.Request;
using Setsis.Service.Services.Category;

namespace Setsis.Api.Controllers
{
    [Authorize]
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            return ActionResultInstance(await _categoryService.GetAllCategoriesAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            return ActionResultInstance(await _categoryService.GetCategoryAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CreateCategoryCommandRequest categoryDto)
        {
            return ActionResultInstance(await _categoryService.AddCategoryAsync(categoryDto));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryCommandRequest updateCategoryDto)
        {
            return ActionResultInstance(await _categoryService.UpdateCategoryAsync(updateCategoryDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return ActionResultInstance(await _categoryService.DeleteCategoryAsync(id));
        }
    }
}
