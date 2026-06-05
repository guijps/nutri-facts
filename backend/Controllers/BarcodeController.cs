using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace NutriFacts.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductApplicationService _ProductApplicationService;

    public ProductController(ProductApplicationService ProductApplicationService)
    {
        _ProductApplicationService = ProductApplicationService;
    }

    [HttpGet("{barcode}")]
    public async Task<IActionResult> GetByBarcode(string barcode)
    {
        try
        {
            var product = await _ProductApplicationService.GetProductByBarcodeAsync(barcode);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }   
        catch (HttpRequestException ex)
        {
            return StatusCode(502, new { message = ex.Message });
        }
        catch (Exception ex)
        {
            // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
            Console.WriteLine($"Error in Search: {ex.Message}");
            return StatusCode(500, "An error occurred while searching for products."); // Return a 500 Internal Server Error
        }
    }

    [HttpGet("/search")]
    public async Task<IActionResult> Search(string query)
    {
        try
        {
            var products = await _ProductApplicationService.GetProductByTextAsync(query);
            return Ok(products);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(502, new { message = ex.Message });
        }
        catch (Exception ex)
        {
            // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
            Console.WriteLine($"Error in Search: {ex.Message}");
            return StatusCode(500, "An error occurred while searching for products."); // Return a 500 Internal Server Error
        }
    }
}
