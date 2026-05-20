public class BarcodeApplicationService
{
    private readonly BarcodeService _service;

    public BarcodeApplicationService(BarcodeService service)
    {
        _service = service;
    }

    public async Task<IProduct?> GetProductByBarcodeAsync(string barcode)
    {
        return await _service.GetProductByBarcodeAsync(barcode);
    }

    public void AddProduct(IProduct product)
    {
        _service.AddProduct(product);
    }
}