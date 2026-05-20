using System;
public class ProductEntry : IProductEntry
{
    public Guid Id { get; set; } = Guid.NewGuid();
    private INutritionFacts _nutritionFacts;
    public INutritionFacts NutritionFacts 
    { 
        get
        {
            return _nutritionFacts;
        }
    }
    public IProduct Product { get; set; }
    private double _quantity;
    public double Quantity { get
        {
            return _quantity;
        }
    set
        {
            if(_quantity == value)
            {
                return;
            }
            _quantity = value;
            UpdateNutritionFacts();
        } }

    public ProductEntry(IProduct product, double quantity)
    {
        Product = product;
        Quantity = quantity;
    }
   public void UpdateNutritionFacts()
    {
        _nutritionFacts = new NutritionFacts
        {
            Carbohydrates = Product.NutritionFacts.Carbohydrates * _quantity,
            Fat = Product.NutritionFacts.Fat * _quantity,
            Proteins = Product.NutritionFacts.Proteins * _quantity,
            Calories = Product.NutritionFacts.Calories * _quantity
        };
    }
}