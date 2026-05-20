using System.Text.Json.Serialization;

public class Nutriments
{
    [JsonPropertyName("energy-kcal_100g")]
    public double Calories { get; set; }
    [JsonPropertyName("proteins_100g")]
    public double Proteins { get; set; }
    [JsonPropertyName("carbohydrates_100g")]
    public double Carbohydrates { get; set; }
    [JsonPropertyName("fat_100g")]
    public double Fat { get; set; }
    [JsonPropertyName("sugars_100g")]
    public double Sugar { get; set; }
    [JsonPropertyName("image_front_small_url")]
    public string Image { get; set; }
}