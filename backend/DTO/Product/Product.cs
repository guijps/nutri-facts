//make this class serializable
public class Product : IProduct
{
    //Todo: make this field obligatory
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Brand { get; set; } = null!;
    public NutritionFacts NutritionFacts { get; set; } = new();

    INutritionFacts IProduct.NutritionFacts
    {
        get => NutritionFacts;
        set => NutritionFacts = value as NutritionFacts ?? new NutritionFacts
        {
            Carbohydrates = value.Carbohydrates,
            Fat = value.Fat,
            Proteins = value.Proteins,
            Calories = value.Calories
        };
    }
}