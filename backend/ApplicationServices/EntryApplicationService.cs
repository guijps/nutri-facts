using NutriFacts.Service;
public class EntryApplicationService(EntryRepository repository, ProductApplicationService productApplicationService)
{
    public async Task<List<IProductEntry>> GetAllAsync(string userId)
    {
        return await repository.GetAllAsync(userId);
    }
    public async Task UpdateAsync(string entryId,string userId, double quantity)
    {
        await repository.UpdateAsync(Guid.Parse(entryId), userId, quantity);
    }
    public async Task AddAsync(string entryId, string userId, double quantity)
    {
        var product = await productApplicationService.GetProductByBarcodeAsync(entryId);
        if (product != null)
        {
            var entry = new ProductEntry(product, quantity)
            {
                UserId = userId
            };
            await repository.AddAsync(entry);
        }
        else
        {
            throw new Exception("Product not found for the given barcode.");
        }
    }
    public async Task DeleteAsync(string entryIdString, string userId)
    {
        var entryId = Guid.Parse(entryIdString);
        await repository.DeleteAsync(entryId, userId);
    }
    public async Task<INutritionFacts> GetTodayFactsAsync(string userId)
    {
        var entries = await repository.GetAllAsync(userId);
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
    public async Task<List<IProduct>> GetHistoryAsync(string userId)
    {
        var history = await repository.GetHistoryAsync(userId);
        return history.Select(e => e.Product).ToList();
    }
}