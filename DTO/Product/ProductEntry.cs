public class ProductEntry : IProductEntry
{
    public string Id { get; set; }
    public INutritionFacts NutritionFacts { get; }
    public IProduct Product { get; set; }
    public double Quantity { get; set; }

    public ProductEntry(string id, IProduct product, double quantity)
    {
        Id = id;
        Product = product;
        Quantity = quantity;
        NutritionFacts = new NutritionFacts
        {
            Carbohydrates = product.NutritionFacts.Carbohydrates * quantity,
            Fat = product.NutritionFacts.Fat * quantity,
            Protein = product.NutritionFacts.Protein * quantity,
            Calories = product.NutritionFacts.Calories * quantity
        };
    }
    public INutritionFacts GetNutritionFacts()
    {
        return NutritionFacts;
    }
}