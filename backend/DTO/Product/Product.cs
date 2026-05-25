//make this class serializable
public class Product : IProduct
{
    //Todo: make this field obligatory
    public string Id { get; set; }
    public string Name { get; set; }
    public INutritionFacts? NutritionFacts { get; set; }
}