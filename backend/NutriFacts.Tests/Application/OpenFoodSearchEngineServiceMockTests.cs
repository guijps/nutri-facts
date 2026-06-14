using NutriFacts.Service.Parser.OpenFood;
using NutriFacts.Tests.Mocks;
using Xunit;

namespace NutriFacts.Tests.Application;

/// <summary>
/// Tests for OpenFoodSearchEngineService using mocked HTTP responses.
/// These tests are deterministic and don't depend on external APIs.
/// </summary>
public class OpenFoodSearchEngineServiceMockTests
{
    private readonly string _testDataDirectory;

    public OpenFoodSearchEngineServiceMockTests()
    {
        _testDataDirectory = Path.Combine(AppContext.BaseDirectory, "NutriFacts.Tests");
    }

    [Fact]
    public async Task SearchByBarcodeAsync_WithValidBarcode_ReturnsParsedProduct()
    {
        // Arrange
        var barcode = "3017620422003";
        var jsonFilePath = Path.Combine(_testDataDirectory, "openfoods_data_parse_1.json");
        
        var mockHandler = new MockHttpMessageHandler();
        mockHandler.RegisterBarcodeResponse(barcode, jsonFilePath);

        var httpClient = new HttpClient(mockHandler);
        var parser = new OpenFoodParser();
        var searchEngine = new OpenFoodSearchEngineService(httpClient, parser);

        // Act
        var product = await searchEngine.SearchByBarcodeAsync(barcode);

        // Assert
        Assert.NotNull(product);
        Assert.Equal("Flocons d'avoine", product.Name);
        Assert.Equal(372, product.NutritionFacts.Calories);
    }

    [Fact]
    public async Task SearchByBarcodeAsync_WithInvalidBarcode_Returns404()
    {
        // Arrange
        var invalidBarcode = "0000000000000";
        
        var mockHandler = new MockHttpMessageHandler();
        var httpClient = new HttpClient(mockHandler);
        var parser = new OpenFoodParser();
        var searchEngine = new OpenFoodSearchEngineService(httpClient, parser);

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(
            () => searchEngine.SearchByBarcodeAsync(invalidBarcode));
    }

    [Fact]
    public async Task SearchByTextAsync_WithValidQuery_ReturnsListOfProducts()
    {
        // Arrange
        var searchQuery = "chocolate";
        var jsonFilePath = Path.Combine(_testDataDirectory, "openfoods_list_data_parse.json");
        
        var mockHandler = new MockHttpMessageHandler();
        mockHandler.RegisterTextSearchResponse(searchQuery, jsonFilePath);

        var httpClient = new HttpClient(mockHandler);
        var parser = new OpenFoodParser();
        var searchEngine = new OpenFoodSearchEngineService(httpClient, parser);

        // Act
        var products = await searchEngine.SearchByTextAsync(searchQuery);

        // Assert
        Assert.NotNull(products);
        Assert.NotEmpty(products);
    }

    [Fact]
    public async Task SearchByTextAsync_WithEmptyResult_ThrowsException()
    {
        // Arrange
        var searchQuery = "nonexistent_product_xyz";
        
        var mockHandler = new MockHttpMessageHandler();
        // No response registered - will return 404
        
        var httpClient = new HttpClient(mockHandler);
        var parser = new OpenFoodParser();
        var searchEngine = new OpenFoodSearchEngineService(httpClient, parser);

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(
            () => searchEngine.SearchByTextAsync(searchQuery));
    }
}
