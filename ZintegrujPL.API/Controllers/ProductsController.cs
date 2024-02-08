using Microsoft.AspNetCore.Mvc;
using ZintegrujPL.Core.Interfaces.Services;

namespace ZintegrujPL.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet("{sku}")]
        public async Task<IActionResult> GetProductDetails(string sku)
        {
            try
            {
                _logger.LogInformation($"Retrieving product details for SKU: {sku}");

                var productDetails = await _productService.GetProductDetailsBySKUAsync(sku);
                if (productDetails == null)
                {
                    _logger.LogWarning($"Product with SKU: {sku} not found.");
                    return NotFound($"Product with SKU: {sku} not found.");
                }

                _logger.LogInformation($"Product details for SKU: {sku} retrieved successfully.");
                return Ok(productDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving product details for SKU: {sku}.");
                return StatusCode(500, "An error occurred while retrieving the product details.");
            }
        }
    }
}
