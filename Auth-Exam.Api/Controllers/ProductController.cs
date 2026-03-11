using Auth_Exam.Core.Contracts;
using Auth_Exam.Core.Models.Product;
using Auth_Exam.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth_Exam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            var products = await _productService.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Product>> CreateProduct(CreateProductFormViewModel model)
        {
            var product = await _productService.CreateProductAsync(model);
            return Created($"/api/product/{product.Id}", product);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<bool>> UpdateProduct(string id, UpdateProductFormViewModel model)
        {
            model.Id = id;
            var success = await _productService.UpdateProductAsync(model);
            if (!success)
            {
                return NotFound();
            }
            return Ok(success);
        }
    }
}
