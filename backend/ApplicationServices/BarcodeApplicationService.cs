public class BarcodeApplicationService
{
    private readonly BarcodeService _service;

    public BarcodeApplicationService(BarcodeService service)
    {
        _service = service;
    }

    public IProduct? GetProductByBarcode(string barcode)
    {
        return _service.GetProductByBarcode(barcode);
    }

    public void AddProduct(IProduct product)
    {
        _service.AddProduct(product);
    }
}