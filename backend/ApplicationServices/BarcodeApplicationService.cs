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
    
    public async Task<List<IProduct>?> GetProductByTextAsync(string text)
    {
        return await _service.GetProductByTextAsync(text);
    }

    public void AddProduct(IProduct product)
    {
        _service.AddProduct(product);
    }
}