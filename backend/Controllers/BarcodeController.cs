using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace NutriFacts.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class BarcodeController : ControllerBase
{
    private readonly BarcodeApplicationService _barcodeApplicationService;

    public BarcodeController(BarcodeApplicationService barcodeApplicationService)
    {
        _barcodeApplicationService = barcodeApplicationService;
    }

    [HttpGet("{barcode}")]
    public async Task<IActionResult> GetByBarcode(string barcode)
    {
        try
        {
            var product = await _barcodeApplicationService.GetProductByBarcodeAsync(barcode);
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
            var products = await _barcodeApplicationService.GetProductByTextAsync(query);
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
