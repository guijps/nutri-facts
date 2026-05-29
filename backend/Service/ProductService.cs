
namespace NutriFacts.Service;
public class ProductService
{
    private readonly ProductRepository _repository;
    private readonly OpenFoodSearchEngineService _searchEngine;

    public ProductService(ProductRepository repository,OpenFoodSearchEngineService searchEngine)
    {
        _repository = repository;
        _searchEngine = searchEngine;
    }

    public async Task<IProduct?> GetProductByBarcodeAsync(string barcode)
    {
        var product = _repository.GetByBarcode(barcode);
        if (product != null)
        {
            return product;
        }
        var searchedProduct = await _searchEngine.SearchByBarcodeAsync(barcode);
        if (searchedProduct != null)
        {
            _repository.AddProduct(searchedProduct);
            return searchedProduct;
        }
        return null;
    }
    public async Task<List<IProduct>?> GetProductByTextAsync(string text)
    {

        var searchedProduct = await _searchEngine.SearchByTextAsync(text);
        if (searchedProduct != null && searchedProduct.Count > 0)
        {
            return searchedProduct;
        }
        return null;
    }
    public void AddProduct(IProduct product)
    {
        _repository.AddProduct(product);
    }
}
