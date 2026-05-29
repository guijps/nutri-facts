using NutriFacts.Service;
public class ProductApplicationService
{
    private readonly ProductService _service;

    public ProductApplicationService(ProductService service)
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