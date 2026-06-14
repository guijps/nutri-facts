using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace NutriFacts.Controllers;

[ApiController]
[Authorize]
[Route("api/product")]
public class ProductController : ControllerBase
{
    private readonly ProductApplicationService _productApplicationService;

    public ProductController(ProductApplicationService productApplicationService)
    {
        _productApplicationService = productApplicationService;
    }

    [HttpGet("{barcode}")]
    public async Task<IActionResult> GetByBarcode(string barcode)
    {
            var product = await _productApplicationService.GetProductByBarcodeAsync(barcode);
            return Ok(product);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string query)
    {
        var products = await _productApplicationService.GetProductByTextAsync(query);
        return Ok(products);
    }
}
