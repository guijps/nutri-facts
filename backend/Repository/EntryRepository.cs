using Microsoft.EntityFrameworkCore;

public class EntryRepository
{
    private readonly AppDbContext _db;

    public EntryRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IProductEntry?> GetByIdAsync(Guid id, string userId)
    {
        return await _db.ProductEntries
            .Include(entry => entry.Product)
            .FirstOrDefaultAsync(entry => entry.Id == id && entry.UserId == userId);
    }
    public async Task<List<IProductEntry>> GetHistoryAsync(string userId)
    {
        var entries = await _db.ProductEntries
            .AsNoTracking()
            .Include(entry => entry.Product)
            .Where(entry => entry.UserId == userId)
            .ToListAsync();

        return entries.Cast<IProductEntry>().ToList();
    }

    public async Task AddAsync(IProductEntry entry)
    {
        var productEntry = entry as ProductEntry ?? throw new ArgumentException("EntryRepository requires ProductEntry entities.", nameof(entry));

        await _db.ProductEntries.AddAsync(productEntry);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid entryId, string userId)
    {
        var entry = await _db.ProductEntries.FirstOrDefaultAsync(productEntry => productEntry.Id == entryId && productEntry.UserId == userId);
        if (entry != null)
        {
            _db.ProductEntries.Remove(entry);
            await _db.SaveChangesAsync();
        }
    }

    public async Task UpdateAsync(Guid entryId, string userId, double quantity)
    {
        var entry = await _db.ProductEntries.FirstOrDefaultAsync(productEntry => productEntry.Id == entryId && productEntry.UserId == userId);
        if (entry == null)
        {
            return;
        }

        entry.Quantity = quantity;
        await _db.SaveChangesAsync();
    }

    public async Task<List<IProductEntry>> GetAllAsync(string userId)
    {
        var entries = await _db.ProductEntries
            .AsNoTracking()
            .Include(entry => entry.Product)
            .Where(entry => entry.UserId == userId)
            .ToListAsync();

        return entries.Cast<IProductEntry>().ToList();
    }
}