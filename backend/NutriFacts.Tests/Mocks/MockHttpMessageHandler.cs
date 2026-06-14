using System.Net;
using System.Text;

namespace NutriFacts.Tests.Mocks;

/// <summary>
/// Mock HttpMessageHandler that returns JSON content from test files.
/// Used to make SearchEngine tests deterministic and network-independent.
/// </summary>
public sealed class MockHttpMessageHandler : HttpMessageHandler
{
    private readonly Dictionary<string, string> _responses;

    public MockHttpMessageHandler()
    {
        _responses = new Dictionary<string, string>();
    }

    /// <summary>
    /// Register a URL pattern to return specific JSON content.
    /// </summary>
    public void RegisterResponse(string urlPattern, string jsonContent)
    {
        _responses[urlPattern] = jsonContent;
    }

    /// <summary>
    /// Load JSON content from a test data file and register it for a URL pattern.
    /// </summary>
    public void RegisterResponseFromFile(string urlPattern, string filePath)
    {
        var content = File.ReadAllText(filePath);
        RegisterResponse(urlPattern, content);
    }

    /// <summary>
    /// Register a barcode search response from test data.
    /// </summary>
    public void RegisterBarcodeResponse(string barcode, string jsonFilePath)
    {
        var content = File.ReadAllText(jsonFilePath);
        RegisterResponse(barcode, content);
    }

    /// <summary>
    /// Register a text search response from test data.
    /// </summary>
    public void RegisterTextSearchResponse(string searchTerm, string jsonFilePath)
    {
        var content = File.ReadAllText(jsonFilePath);
        RegisterResponse(searchTerm, content);
    }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var uri = request.RequestUri?.ToString() ?? string.Empty;

        // Try to find a matching registered response
        var matchingKey = _responses.Keys.FirstOrDefault(key =>
            uri.Contains(key, StringComparison.OrdinalIgnoreCase));

        if (matchingKey != null && _responses.TryGetValue(matchingKey, out var jsonContent))
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
            };

            return Task.FromResult(response);
        }

        // Return 404 for unregistered URLs
        return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound)
        {
            Content = new StringContent("{\"error\": \"Not found\"}", Encoding.UTF8, "application/json")
        });
    }
}
