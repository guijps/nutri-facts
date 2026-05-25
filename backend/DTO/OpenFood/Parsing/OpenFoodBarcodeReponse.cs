using System.Text.Json.Serialization;

public class OpenFoodBarcodeResponse
{
    [JsonPropertyName("code")]
    public string Id { get; set; }
    
    [JsonPropertyName("product")]
    public ProductData? Product { get; set; }
    
}