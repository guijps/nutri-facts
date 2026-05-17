public class BarcodeService
{
    private readonly BarcodeRepository _repository;

    public BarcodeService(BarcodeRepository repository)
    {
        _repository = repository;
    }

    public IProduct? GetProductByBarcode(string barcode)
    {
        return _repository.GetByBarcode(barcode);
    }

    public void AddProduct(IProduct product)
    {
        _repository.AddProduct(product);
    }
}
