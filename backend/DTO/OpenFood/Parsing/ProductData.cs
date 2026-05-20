using System.Text.Json.Serialization;

public class ProductData
{
    [JsonPropertyName("nutriments")]
    public Nutriments Nutriments { get; set; }
    
    [JsonPropertyName("product_name")]
    public string Name { get; set; }
}