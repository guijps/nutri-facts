using System.Text.Json.Serialization;

public class OpenFoodSearchListItem
{
    [JsonPropertyName("code")]
    public string Id { get; set; }
    
    [JsonPropertyName("nutriments")]
    public Nutriments Nutriments { get; set; }
    
    [JsonPropertyName("product_name")]
    public string Name { get; set; }
}