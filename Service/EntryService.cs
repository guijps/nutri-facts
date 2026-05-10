public class EntryService
{
    private readonly EntryRepository _entryRepository;

    public EntryService(EntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }
    public List<IProductEntry> GetAllEntries()
    {
        return _entryRepository.GetAllEntries();
    }

    public void AddEntry(IProductEntry entry)
    {
        _entryRepository.AddEntry(entry);
    }
}