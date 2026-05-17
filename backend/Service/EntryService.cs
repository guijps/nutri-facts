public class EntryService
{
    private readonly EntryRepository _entryRepository;

    public EntryService(EntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }
    public List<IProductEntry> GetAll()
    {
        return _entryRepository.GetAll();
    }

    public void Add(IProductEntry entry)
    {
        _entryRepository.Add(entry);
    }
    public void Delete(Guid entryId)
    {
        _entryRepository.Delete(entryId);
    }
    public void Update(string id,double quantity)
    {
        var entry = _entryRepository.GetById(Guid.Parse(id));
        if (entry != null)
        {
            entry.Quantity = quantity;
        }
    }
}