public class EntryApplicationService
{
    private readonly EntryService _service;
    private readonly BarcodeService _barcodeService;

    public EntryApplicationService(EntryService service, BarcodeService barcodeService)
    {
        _service = service;
        _barcodeService = barcodeService;
    }

    public List<IProductEntry> GetAll()
    {
        return _service.GetAll();
    }
    public void Update(string entryId, double quantity)
    {
        _service.Update(entryId, quantity);
    }
    public async Task AddAsync(string entryId, double quantity)
    {
        var product = await _barcodeService.GetProductByBarcodeAsync(entryId);
        if (product != null)
        {
            var entry = new ProductEntry(product, quantity);
            _service.Add(entry);
        }
        else
        {
            throw new Exception("Product not found for the given barcode.");
        }
    }
    public void Delete(string entryIdString)
    {
        var entryId = Guid.Parse(entryIdString);
        _service.Delete(entryId);
    }
    public INutritionFacts GetTodayFacts()
    {
        var entries = _service.GetAll();
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
}