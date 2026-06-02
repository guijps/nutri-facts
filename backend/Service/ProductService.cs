
using Microsoft.Extensions.Logging;

namespace NutriFacts.Service;
public class ProductService
{
    private readonly ProductRepository _repository;
    private readonly OpenFoodSearchEngineService _searchEngine;
    private readonly ILogger<ProductService> _logger;

    public ProductService(ProductRepository repository, OpenFoodSearchEngineService searchEngine, ILogger<ProductService> logger)
    {
        _repository = repository;
        _searchEngine = searchEngine;
        _logger = logger;
    }

    public async Task<IProduct?> GetProductByBarcodeAsync(string barcode)
    {
        _logger.LogDebug("Looking up product by barcode: {Barcode}", barcode);

        var product = await _repository.GetByBarcodeAsync(barcode);
        if (product != null)
        {
            _logger.LogInformation("Product found in cache: {Barcode}", barcode);
            return product;
        }

        _logger.LogWarning("Product not found for barcode: {Barcode}", barcode);
        return null;
    }

    public async Task<List<IProduct>?> GetProductByTextAsync(string text)
    {
        _logger.LogDebug("Searching products by text: {Text}", text);

        var searchedProduct = await _repository.GetByTextAsync(text);
        if (searchedProduct != null && searchedProduct.Count > 0)
        {
            _logger.LogInformation("Text search returned {Count} result(s) for: {Text}", searchedProduct.Count, text);
            return searchedProduct;
        }

        _logger.LogWarning("No products found for text: {Text}", text);
        return null;
    }

    public void AddProduct(IProduct product)
    {
        _logger.LogInformation("Adding product: {Name} ({Id})", product.Name, product.Id);
        _repository.AddProduct(product);
    }
}
