public interface IProductEntry
{
    string Id { get; set; }
    IProduct Product { get; set; }
    double Quantity { get; set; }

    INutritionFacts GetNutritionFacts();
}