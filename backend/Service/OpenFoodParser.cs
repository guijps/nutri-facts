using System.Text.Json;

public class OpenFoodParser
{
    
    public IProduct? Parse(string json)
    {
        try
        {
            var productData = JsonSerializer.Deserialize<OpenFoodReponse>(json);
            if (productData != null && productData.Product != null)
            {
                return new Product
                {
                    Id = productData.Id,
                    Name = productData.Product.Name,
                    NutritionFacts = new NutritionFacts
                    {
                        Calories = productData.Product.Nutriments.Calories,
                        Carbohydrates = productData.Product.Nutriments.Carbohydrates,
                        Proteins = productData.Product.Nutriments.Proteins,
                        Fat = productData.Product.Nutriments.Fat
                    }
                };
            }
        }
        catch (JsonException)
        {
            // Handle JSON parsing errors
            Console.WriteLine("Error parsing JSON data.");
        }
        return null;
    }
}