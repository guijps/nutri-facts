using NutriFacts.Service;
public class EntryApplicationService
{
    private readonly EntryService _service;
    private readonly ProductService _ProductService;

    public EntryApplicationService(EntryService service, ProductService ProductService)
    {
        _service = service;
        _ProductService = ProductService;
    }

    public List<IProductEntry> GetAll(string userId)
    {
        return _service.GetAll(userId);
    }
    public void Update(string entryId,string userId, double quantity)
    {
        _service.Update(entryId, userId, quantity);
    }
    public async Task AddAsync(string entryId, string userId, double quantity)
    {
        var product = await _ProductService.GetProductByBarcodeAsync(entryId);
        if (product != null)
        {
            var entry = new ProductEntry(product, quantity)
            {
                UserId = userId
            };
            _service.Add(entry);
        }
        else
        {
            throw new Exception("Product not found for the given barcode.");
        }
    }
    public void Delete(string entryIdString, string userId)
    {
        var entryId = Guid.Parse(entryIdString);
        _service.Delete(entryId, userId);
    }
    public INutritionFacts GetTodayFacts(string userId)
    {
        var entries = _service.GetAll(userId);
        // Logic to filter entries for today and calculate nutrition facts
        double totalCarbs = entries.Sum(e => e.NutritionFacts.Carbohydrates);
        double totalFat = entries.Sum(e => e.NutritionFacts.Fat);
        double totalProtein = entries.Sum(e => e.NutritionFacts.Proteins);
        double totalCalories = entries.Sum(e => e.NutritionFacts.Calories);


        return new NutritionFacts
        {
            Carbohydrates = totalCarbs,
            Fat = totalFat,
            Proteins = totalProtein,
            Calories = totalCalories
        };
    }

    public List<IProduct> GetHistory(string userId)
    {
        return _service.GetHistory(userId).Select(e => e.Product).ToList();
    }
}