using Microsoft.AspNetCore.Mvc;

namespace NutriFacts.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BarcodeController : ControllerBase
{
    private readonly BarcodeApplicationService _barcodeApplicationService;

    public BarcodeController(BarcodeApplicationService barcodeApplicationService)
    {
        _barcodeApplicationService = barcodeApplicationService;
    }

    [HttpGet("{barcode}")]
    public IActionResult GetByBarcode(string barcode)
    {
        var product = _barcodeApplicationService.GetProductByBarcode(barcode);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }
}
