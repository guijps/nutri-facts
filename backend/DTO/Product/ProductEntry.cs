using System;
using System.ComponentModel.DataAnnotations.Schema;
public class ProductEntry : IProductEntry
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserId { get; set; } = null!;
    public string ProductId { get; set; } = null!;
    public Product Product { get; set; } = null!;
    public double Quantity { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [NotMapped]
    public NutritionFacts NutritionFacts
    { 
        get
        {
            var baseFacts = Product?.NutritionFacts;
            if (baseFacts == null)
            {
                return new NutritionFacts();
            }

            return new NutritionFacts
            {
                Carbohydrates = baseFacts.Carbohydrates * Quantity,
                Fat = baseFacts.Fat * Quantity,
                Proteins = baseFacts.Proteins * Quantity,
                Calories = baseFacts.Calories * Quantity
            };
        }
    }

    IProduct IProductEntry.Product
    {
        get => Product;
        set
        {
            Product = value as Product ?? throw new ArgumentException("ProductEntry requires a Product entity.", nameof(value));
            ProductId = Product.Id;
        }
    }

    INutritionFacts IProductEntry.NutritionFacts => NutritionFacts;

    public ProductEntry()
    {
    }

    public ProductEntry(IProduct product, double quantity)
    {
        ((IProductEntry)this).Product = product;
        Quantity = quantity;
    }
}