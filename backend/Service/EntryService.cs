namespace NutriFacts.Service;
public class EntryService
{
    private readonly EntryRepository _entryRepository;

    public EntryService(EntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }
    public List<IProductEntry> GetAll(string userId)
    {
        return _entryRepository.GetAll(userId);
    }

    public void Add(IProductEntry entry)
    {
        _entryRepository.Add(entry);
    }
    public void Delete(Guid entryId, string userId)
    {
        _entryRepository.Delete(entryId, userId);
    }
    public void Update(string id, string userId, double quantity)
    {
        _entryRepository.Update(Guid.Parse(id), userId, quantity);
    }

    public List<IProductEntry> GetHistory(string userId)
    {
        return _entryRepository.GetHistory(userId);
    }
}