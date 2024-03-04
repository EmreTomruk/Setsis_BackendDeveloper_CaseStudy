using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Setsis.Infrastructure.CQRS.Commands.Products.Request;
using Setsis.Service.Services.Product;

namespace Setsis.Api.Controllers
{
    [Authorize]
    [Route("api/products")]
    [ApiController]
    public class ProductsController : CustomBaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return ActionResultInstance(await _productService.GetAllProductsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            return ActionResultInstance(await _productService.GetProductAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(CreateProductCommandRequest productDto)
        {
            return ActionResultInstance(await _productService.AddProductAsync(productDto));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommandRequest updateProductDto)
        {
            return ActionResultInstance(await _productService.UpdateProductAsync(updateProductDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return ActionResultInstance(await _productService.DeleteProductAsync(id));
        }
    }
}
