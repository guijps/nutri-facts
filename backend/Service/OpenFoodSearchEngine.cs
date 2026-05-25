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
        var response = await _httpClient.GetAsync($"{Parameters.OpenFoodBarcodeApiUrl}{barcode}.json");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var content = await response.Content.ReadAsStringAsync();
        var product = _parser.Parse(content);
        return product;
    }

    
    public async Task<List<IProduct>?> SearchByTextAsync(string text)
    {
        /*
        https://world.openfoodfacts.org/cgi/search.pl?
        // search_terms=chocolate&
        // brands=nestle&
        // search_simple=1&
        // action=process&
        // json=1&
        // page_size=10
        // */
        var searchTerms = text.Replace(" ", "+");
        var defaultParams = "search_simple=1&action=process&json=1&page_size=3";
        string path = $"{Parameters.OpenFoodSearchUrl}";
        path += $"search.pl?search_terms={searchTerms}&{defaultParams}";
        var response = 
        await _httpClient.GetAsync(path);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var content = await response.Content.ReadAsStringAsync();
        var products = _parser.ParseList(content);
        return products;
    }
}