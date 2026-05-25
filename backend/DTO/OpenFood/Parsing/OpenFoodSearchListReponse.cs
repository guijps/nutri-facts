using System.Text.Json.Serialization;

public class OpenFoodSearchListReponse
{
    [JsonPropertyName("page")]
    public int Page { get; set; }
    [JsonPropertyName("count")]
    public int Count { get; set; }
    [JsonPropertyName("page_count")]
    public int PageCount { get; set; }
    [JsonPropertyName("page_size")]
    public int PageSize { get; set; }
    
    [JsonPropertyName("products")]
    public List<OpenFoodSearchListItem>? Products { get; set; }
    
}