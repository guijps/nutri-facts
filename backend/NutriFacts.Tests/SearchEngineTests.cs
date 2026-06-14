using NutriFacts.Service.Parser.OpenFood;
using NutriFacts.Tests.Mocks;
using Xunit;

public class SearchEngineTests
{
    [Fact]
    public async Task SearchByTextAsync_ValidText_ReturnsProducts()
    {
        var text = "chocolate";
        var mockHandler = new MockHttpMessageHandler();
        mockHandler.RegisterResponseFromFile(text, ResolveFixturePath("openfoods_list_data_parse.json"));

        var httpClient = new HttpClient(mockHandler);
        var parser = new OpenFoodParser();
        var searchEngine = new OpenFoodSearchEngineService(httpClient, parser);

        var products = await searchEngine.SearchByTextAsync(text);

        Assert.NotNull(products);
        Assert.True(products!.Count > 0);
        Assert.All(products, p => Assert.False(string.IsNullOrWhiteSpace(p.Name)));
    }


    [Fact]
    public async Task SearchByBarcodeAsync_ValidBarcode_ReturnsProduct()
    {
        var barcode = "3017620422003";
        var mockHandler = new MockHttpMessageHandler();
        mockHandler.RegisterResponseFromFile(barcode, ResolveFixturePath("openfoods_data_parse_1.json"));

        var httpClient = new HttpClient(mockHandler);
        var parser = new OpenFoodParser();
        var searchEngine = new OpenFoodSearchEngineService(httpClient, parser);

        var product = await searchEngine.SearchByBarcodeAsync(barcode);

        Assert.NotNull(product);
        Assert.Equal("Flocons d'avoine", product!.Name);
    }

    [Fact]
    public async Task SearchByBarcodeAsync_InvalidBarcode_ThrowsHttpRequestException()
    {
        var barcode = "0000000000000"; // Invalid barcode
        var mockHandler = new MockHttpMessageHandler();
        var httpClient = new HttpClient(mockHandler);
        var parser = new OpenFoodParser();
        var searchEngine = new OpenFoodSearchEngineService(httpClient, parser);

        await Assert.ThrowsAsync<HttpRequestException>(() => searchEngine.SearchByBarcodeAsync(barcode));
    }

    private static string ResolveFixturePath(string fileName)
    {
        return Path.Combine(AppContext.BaseDirectory, fileName)
            .Replace("bin\\Debug\\net10.0", string.Empty)
            .Replace("bin\\Release\\net10.0", string.Empty);
    }
}