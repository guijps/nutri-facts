public class EntryRepository
{
    private readonly List<IProductEntry> _entries = new List<IProductEntry>();

    public IProductEntry? GetById(string id)
    {
        return _entries.FirstOrDefault(e => e.Id == id);
    }

    public void AddEntry(IProductEntry entry)
    {
        _entries.Add(entry);
    }
    public List<IProductEntry> GetAllEntries()
    {
        return _entries;
    }
}