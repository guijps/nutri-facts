using NutriFacts.Domain.Exceptions;
public class ProductApplicationService(
    ProductRepository repository, 
    ILogger<ProductApplicationService> logger)
{
    public async Task<IProduct?> GetProductByBarcodeAsync(string barcode)
    {
        logger.LogDebug("Looking up product by barcode: {0}", barcode);

        var product = await repository.GetByBarcodeAsync(barcode);
        if (product != null)
        {
            logger.LogInformation("Product found in cache: {0}", barcode);
            return product;
        }
        throw new ProductNotFoundException(barcode);
    }
    
    public async Task<IEnumerable<IProduct>?> GetProductByTextAsync(string text)
    {
        logger.LogDebug("Searching products by text: {0}", text);

        var searchedProduct = await repository.GetByTextAsync(text);
        if (searchedProduct != null && searchedProduct.Any())
        {
            logger.LogInformation("Text search returned {0} result(s) for: {1}", searchedProduct.Count(), text);
            return searchedProduct;
        }

        logger.LogWarning("No products found for text: {0}", text);
        throw new ProductNotFoundException(text);
    }

    public void AddProduct(IProduct product)
    {
        
        logger.LogInformation("Adding product: {0} ({1})", product.Name, product.Id);
        repository.AddProduct(product);
    }
}