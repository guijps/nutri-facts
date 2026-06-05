using System.Text.Json;

namespace NutriFacts.Service.Parser.OpenFood;  
public class OpenFoodParser
{
    public IProduct?  Parse(string json)
    {
        try
        {
            var productData = JsonSerializer.Deserialize<OpenFoodSearchItem>(json);
            if (productData != null && productData.Product != null && productData.Product.Nutriments != null)
            {
                return new Product
                {
                    Id = productData.Id,
                    Name = productData.Product.Name,
                    Brand = productData.Product.Brand,
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
    public List<IProduct> ParseList(string json)
    {
        try
        {
                List<IProduct> products = new List<IProduct>();
            var productData = JsonSerializer.Deserialize<OpenFoodSearchListReponse>(json);
            if (productData != null && productData.Products != null && productData.Products.Count > 0)
            {

                foreach (var p in productData.Products)
                {
                    if (p.Nutriments == null)
                    {
                        Console.WriteLine($"Warning: Product '{p.Name}' does not have nutriments data.");
                        continue;
                    }
                    products.Add(ParseToProduct(p));
                }
                return products;
            }
        }
        catch (JsonException)
        {
            // Handle JSON parsing errors
            Console.WriteLine("Error parsing JSON data.");
        }
        return null;
    }

    public IProduct ParseToProduct(OpenFoodSearchListItem p)
    {
        return new Product
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Brand= p.Brand,
                            NutritionFacts = new NutritionFacts
                            {
                                Calories = p.Nutriments.Calories,
                                Carbohydrates = p.Nutriments.Carbohydrates,
                                Proteins = p.Nutriments.Proteins,
                                Fat = p.Nutriments.Fat
                            }
                    };
    }
    
    public IProduct ParseToProduct(OpenFoodSearchItem p)
    {
        return new Product
                        {
                            Id = p.Id,
                            Name = p.Product.Name,
                            Brand= p.Product.Brand,
                            NutritionFacts = new NutritionFacts
                            {
                                Calories = p.Product.Nutriments.Calories,
                                Carbohydrates = p.Product.Nutriments.Carbohydrates,
                                Proteins = p.Product.Nutriments.Proteins,
                                Fat = p.Product.Nutriments.Fat
                            }
                    };
    }
    
}