using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Setsis.Service.Services.Category;
using Setsis.Service.Services.Category.Dto;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            return ActionResultInstance(await _categoryService.GetCategoryAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryDto categoryDto)
        {
            return ActionResultInstance(await _categoryService.AddCategoryAsync(categoryDto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDto updateCategoryDto)
        {
            return ActionResultInstance(await _categoryService.UpdateCategoryAsync(id, updateCategoryDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return ActionResultInstance(await _categoryService.DeleteCategoryAsync(id));
        }
    }
}
