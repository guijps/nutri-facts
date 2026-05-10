public class EntryApplicationService
{
    private readonly EntryService _service;
    private readonly BarcodeService _barcodeService;

    public EntryApplicationService(EntryService service, BarcodeService barcodeService)
    {
        _service = service;
        _barcodeService = barcodeService;
    }

    public List<IProductEntry> GetAllEntries()
    {
        return _service.GetAllEntries();
    }

    public void AddEntry(string entryId, double quantity)
    {
        var product = _barcodeService.GetProductByBarcode(entryId);
        var entry = new ProductEntry(entryId, product, quantity);
        _service.AddEntry(entry);
    }
    public INutritionFacts GetTodayFacts()
    {
        var entries = _service.GetAllEntries();
        // Logic to filter entries for today and calculate nutrition facts
        double totalCarbs = entries.Sum(e => e.GetNutritionFacts().Carbohydrates);
        double totalFat = entries.Sum(e => e.GetNutritionFacts().Fat);
        double totalProtein = entries.Sum(e => e.GetNutritionFacts().Protein);
        double totalCalories = entries.Sum(e => e.GetNutritionFacts().Calories);


        return new NutritionFacts
        {
            Carbohydrates = totalCarbs,
            Fat = totalFat,
            Protein = totalProtein,
            Calories = totalCalories
        };
    }
}