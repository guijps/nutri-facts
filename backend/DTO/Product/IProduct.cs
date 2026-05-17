public interface IProduct
{
    string Id { get; set; }
    string Name { get; set; }
    public INutritionFacts NutritionFacts { get; set; }
}