public interface IProductEntry
{
    Guid Id { get; set; }
    IProduct Product { get; set; }
    double Quantity { get; set; }
    INutritionFacts NutritionFacts { get; }
    DateTime CreatedAt { get; set; }

}