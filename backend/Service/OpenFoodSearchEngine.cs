using System.Text.Json;

public class OpenFoodSearchEngine
{
    private readonly HttpClient _httpClient;
    private readonly OpenFoodParser _parser;

    public OpenFoodSearchEngine(HttpClient httpClient, OpenFoodParser parser)
    {
        _httpClient = httpClient;
        _parser = parser;
    }

    public async Task<IProduct?> SearchByBarcodeAsync(string barcode)
    {
        var response = await _httpClient.GetAsync($"{Parameters.OpenFoodApiUrl}{barcode}.json");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var content = await response.Content.ReadAsStringAsync();
        var product = _parser.Parse(content);
        return product;
    }
}