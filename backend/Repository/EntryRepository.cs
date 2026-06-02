using Microsoft.EntityFrameworkCore;

public class EntryRepository
{
    private readonly AppDbContext _db;

    public EntryRepository(AppDbContext db)
    {
        _db = db;
    }

    public IProductEntry? GetById(Guid id, string userId)
    {
        return _db.ProductEntries
            .Include(entry => entry.Product)
            .FirstOrDefault(entry => entry.Id == id && entry.UserId == userId);
    }
    public List<IProductEntry> GetHistory(string userId)
    {
        return _db.ProductEntries
            .AsNoTracking()
            .Include(entry => entry.Product)
            .Where(entry => entry.UserId == userId)
            .Cast<IProductEntry>()
            .ToList();
    }

    public void Add(IProductEntry entry)
    {
        var productEntry = entry as ProductEntry ?? throw new ArgumentException("EntryRepository requires ProductEntry entities.", nameof(entry));

        _db.ProductEntries.Add(productEntry);
        _db.SaveChanges();
    }

    public void Delete(Guid entryId, string userId)
    {
        var entry = _db.ProductEntries.FirstOrDefault(productEntry => productEntry.Id == entryId && productEntry.UserId == userId);
        if (entry != null)
        {
            _db.ProductEntries.Remove(entry);
            _db.SaveChanges();
        }
    }

    public void Update(Guid entryId, string userId, double quantity)
    {
        var entry = _db.ProductEntries.FirstOrDefault(productEntry => productEntry.Id == entryId && productEntry.UserId == userId);
        if (entry == null)
        {
            return;
        }

        entry.Quantity = quantity;
        _db.SaveChanges();
    }

    public List<IProductEntry> GetAll(string userId)
    {
        return _db.ProductEntries
            .AsNoTracking()
            .Include(entry => entry.Product)
            .Where(entry => entry.UserId == userId)
            .Cast<IProductEntry>()
            .ToList();
    }
}