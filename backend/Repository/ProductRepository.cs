using Microsoft.EntityFrameworkCore;

public class ProductRepository
{
    private readonly AppDbContext _db;
    private readonly OpenFoodSearchEngineService _searchEngine;
    private readonly ILogger<ProductRepository> _logger;

    public ProductRepository(ILogger<ProductRepository> logger, AppDbContext db, OpenFoodSearchEngineService searchEngine)
    {
        _logger = logger;
        _searchEngine = searchEngine;
        _db = db;
    }

    /// <summary>
    /// Retrieves a product by its barcode. If the product is not found in the database, it will be searched using the OpenFoodSearchEngineService and added to the database if found.
    /// </summary>
    /// <param name="barcode">The barcode of the product to retrieve.</param>
    /// <returns>The product if found; otherwise, null.</returns>
    public async Task<IProduct?> GetByBarcodeAsync(string barcode)
    {
        var existingProduct = await _db.Products.FirstOrDefaultAsync(product => product.Id == barcode);
        if (existingProduct != null)
        {
            _logger.LogDebug("Product with barcode {Barcode} found in the database.", barcode);
            return existingProduct;
        }

        var searchedProduct = await _searchEngine.SearchByBarcodeAsync(barcode);
        if (searchedProduct == null || searchedProduct is not Product)
        {
            return null;
        }

        _db.Products.Add((Product)searchedProduct);
        _logger.LogDebug("Product with barcode {Barcode} added to the database.", barcode);
        await _db.SaveChangesAsync();

        return searchedProduct;
    }

    /// <summary>
    /// Retrieves products by a text search. If the products are not found in the database, they will be searched using the OpenFoodSearchEngineService.
    /// </summary>
    /// <param name="text">The text to search for.</param>
    /// <returns>A list of products if found; otherwise, null.</returns>
    public async Task<List<IProduct>?> GetByTextAsync(string text)
    {
        var cachedProducts = await _db.Products
            .Where(product => product.Name.Contains(text))
            .ToListAsync();

        if (cachedProducts.Count > 0)
        {
            return cachedProducts.Cast<IProduct>().ToList();
        }

        var searchedProduct = await _searchEngine.SearchByTextAsync(text);
        if (searchedProduct == null || searchedProduct.Count == 0)
        {
            return null;
        }
        return searchedProduct;
    }

    /// <summary>
    /// Adds a new product to the database if it does not already exist.
    /// </summary>
    /// <param name="product">The product to add.</param>
    public void AddProduct(IProduct product)
    {
        var persistentProduct = product as Product ?? throw new ArgumentException("ProductRepository requires Product entities.", nameof(product));

        if (!_db.Products.Any(existingProduct => existingProduct.Id == persistentProduct.Id))
        {
            _db.Products.Add(persistentProduct);
            _db.SaveChanges();
        }
    }

}