public class ProductRepository
{
    public Dictionary<string,IProduct> _products = new Dictionary<string, IProduct>
    {
        { "123456789012", new Product { Id = "123456789012", Name = "Apple", NutritionFacts = new NutritionFacts { Calories = 95, Carbohydrates = 25, Proteins = 0.5, Fat = 0.3 } } },
        { "987654321098", new Product { Id = "987654321098", Name = "Banana", NutritionFacts = new NutritionFacts { Calories = 105, Carbohydrates = 27, Proteins = 1.3, Fat = 0.4 } } },
        { "555555555555", new Product { Id = "555555555555", Name = "Orange Juice", NutritionFacts = new NutritionFacts { Calories = 110, Carbohydrates = 26, Proteins = 2, Fat = 0.5 } } }
    };

    public IProduct? GetByBarcode(string barcode)
    {
        // Simulate fetching data from a database or external API
        if (_products.ContainsKey(barcode))
        {
            return _products[barcode];
        }
        
        return null;
    }

    public void AddProduct(IProduct product)
    {
        if (!_products.ContainsKey(product.Id))
        {
            _products.Add(product.Id, product);
        }
    }
}