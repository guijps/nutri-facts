using Xunit;

public class ProductEntryTests
{
    private static Product CreateProduct(double carbs, double fat, double protein, double calories)
    {
        return new Product
        {
            NutritionFacts = new NutritionFacts
            {
                Carbohydrates = carbs,
                Fat = fat,
                Proteins = protein,
                Calories = calories
            }
        };
    }

    [Fact]
    public void ProductEntry_NutritionFacts_AreScaledByQuantity()
    {
        var product = CreateProduct(carbs: 10, fat: 5, protein: 3, calories: 100);
        double quantity = 2.5;

        var entry = new ProductEntry(product, quantity);

        Assert.Equal(25, entry.NutritionFacts.Carbohydrates);
        Assert.Equal(12.5, entry.NutritionFacts.Fat);
        Assert.Equal(7.5, entry.NutritionFacts.Proteins);
        Assert.Equal(250, entry.NutritionFacts.Calories);
    }

    [Fact]
    public void ProductEntry_UpdatingQuantity_RecalculatesNutritionFacts()
    {
        var product = CreateProduct(carbs: 20, fat: 10, protein: 5, calories: 200);
        var entry = new ProductEntry(product, quantity: 1);

        entry.Quantity = 3;

        Assert.Equal(60, entry.NutritionFacts.Carbohydrates);
        Assert.Equal(30, entry.NutritionFacts.Fat);
        Assert.Equal(15, entry.NutritionFacts.Proteins);
        Assert.Equal(600, entry.NutritionFacts.Calories);
        entry.Quantity = 0.5;
        Assert.Equal(10, entry.NutritionFacts.Carbohydrates);
        Assert.Equal(5, entry.NutritionFacts.Fat);
        Assert.Equal(2.5, entry.NutritionFacts.Proteins);
        Assert.Equal(100, entry.NutritionFacts.Calories);
    }
    
    [Fact]
    public void ProductEntry_NullFacts_RecalculatesNutritionFacts()
    {
        var product = new Product{};
        var entry = new ProductEntry(product, quantity: 1);

        Assert.Equal(0, entry.NutritionFacts.Carbohydrates);
        Assert.Equal(0, entry.NutritionFacts.Fat);
        Assert.Equal(0, entry.NutritionFacts.Proteins);
        Assert.Equal(0, entry.NutritionFacts.Calories);
    }
}
