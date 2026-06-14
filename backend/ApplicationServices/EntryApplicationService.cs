using NutriFacts.Service;
using NutriFacts.Domain.Exceptions;
public class EntryApplicationService(EntryRepository repository, ProductApplicationService productApplicationService)
{
    public async Task<IEnumerable<IProductEntry>> GetAllAsync(string userId)
    {
        InputValidation.validateUserId(userId);

        return await repository.GetAllAsync(userId);
    }
    public async Task UpdateAsync(Guid entryId,string userId, double quantity)
    {
        InputValidation.validateUserId(userId);
        InputValidation.validateEntryId(entryId);
        InputValidation.validateQuantity(quantity);

        await repository.UpdateAsync(entryId, userId, quantity);
    }

    public async Task AddAsync(string productId, string userId, double quantity)
    {
        InputValidation.validateUserId(userId);
        InputValidation.validateProductId(productId);
        InputValidation.validateQuantity(quantity);
    
        var product = await productApplicationService.GetProductByBarcodeAsync(productId);
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
            throw new ProductNotFoundException(productId);
        }
    }
    public async Task DeleteAsync(Guid entryId, string userId)
    {
        InputValidation.validateUserId(userId);
        InputValidation.validateEntryId(entryId);

        await repository.DeleteAsync(entryId, userId);
    }
    public async Task<INutritionFacts> GetTodayFactsAsync(string userId)
    {
        InputValidation.validateUserId(userId);

        var entries = await repository.GetAllAsync(userId);
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var todayEntries =  entries.Where(e => 
            DateOnly.FromDateTime(e.CreatedAt) == today).ToList();

        // Logic to filter entries for today and calculate nutrition facts
        double totalCarbs = todayEntries.Sum(e => e.NutritionFacts.Carbohydrates);
        double totalFat = todayEntries.Sum(e => e.NutritionFacts.Fat);
        double totalProtein = todayEntries.Sum(e => e.NutritionFacts.Proteins);
        double totalCalories = todayEntries.Sum(e => e.NutritionFacts.Calories);

        return new NutritionFacts
        {
            Carbohydrates = totalCarbs,
            Fat = totalFat,
            Proteins = totalProtein,
            Calories = totalCalories
        };
    }
    public async Task<IEnumerable<IProduct>> GetHistoryAsync(string userId)
    {
        InputValidation.validateUserId(userId);
        var history = await repository.GetHistoryAsync(userId);
        return history.Select(e => e.Product);
    }

}