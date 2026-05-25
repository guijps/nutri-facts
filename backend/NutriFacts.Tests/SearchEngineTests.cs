using Xunit;

public class SearchEngineTests
{
        OpenFoodSearchEngine _searchEngine;
    public SearchEngineTests()
    {
        HttpClient httpClient = new HttpClient();
        OpenFoodParser parser = new OpenFoodParser();
        _searchEngine = new OpenFoodSearchEngine(httpClient, parser);
    
    }

    [Fact]
    public async Task SearchByTextAsync_ValidText_ReturnsProducts()
    {
        var text = "Nutella"; 

        var products = await _searchEngine.SearchByTextAsync(text);

        Assert.NotNull(products);
        Assert.True(products!.Count > 0);
        Assert.Contains(products, p => p.Name.Contains("Nutella", StringComparison.OrdinalIgnoreCase));
    }


    [Fact]
    public async Task SearchByBarcodeAsync_ValidBarcode_ReturnsProduct()
    {
        var barcode = "3017620422003"; 

        var product = await _searchEngine.SearchByBarcodeAsync(barcode);

        Assert.NotNull(product);
        Assert.Equal("Nutella", product!.Name);
    }

    [Fact]
    public async Task SearchByBarcodeAsync_InvalidBarcode_ReturnsNull()
    {

        var barcode = "0000000000000"; // Invalid barcode

        var product = await _searchEngine.SearchByBarcodeAsync(barcode);

        Assert.Null(product);
    }
}