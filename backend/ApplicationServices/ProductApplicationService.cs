using NutriFacts.Service;
public class ProductApplicationService(
    ProductRepository repository, 
    OpenFoodSearchEngineService searchEngine, 
    ILogger<ProductApplicationService> logger)
{
    public async Task<IProduct?> GetProductByBarcodeAsync(string barcode)
    {
        logger.LogDebug("Looking up product by barcode: {Barcode}", barcode);

        var product = await repository.GetByBarcodeAsync(barcode);
        if (product != null)
        {
            logger.LogInformation("Product found in cache: {Barcode}", barcode);
            return product;
        }

        logger.LogWarning("Product not found for barcode: {Barcode}", barcode);
        return null;
    }
    
    public async Task<List<IProduct>?> GetProductByTextAsync(string text)
    {
        logger.LogDebug("Searching products by text: {Text}", text);

        var searchedProduct = await repository.GetByTextAsync(text);
        if (searchedProduct != null && searchedProduct.Count > 0)
        {
            logger.LogInformation("Text search returned {Count} result(s) for: {Text}", searchedProduct.Count, text);
            return searchedProduct;
        }

        logger.LogWarning("No products found for text: {Text}", text);
        return null;
    }

    public void AddProduct(IProduct product)
    {
        logger.LogInformation("Adding product: {Name} ({Id})", product.Name, product.Id);
        repository.AddProduct(product);
    }
}