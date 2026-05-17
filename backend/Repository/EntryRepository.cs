public class EntryRepository
{
    private readonly List<IProductEntry> _entries = new List<IProductEntry>();

    public IProductEntry? GetById(Guid id)
    {
        return _entries.FirstOrDefault(e => e.Id == id);
    }

    public void Add(IProductEntry entry)
    {
        _entries.Add(entry);
    }

    public void Delete(Guid entryId)
    {
        var entry = GetById(entryId);
        if (entry != null)
        {
            _entries.Remove(entry);
        }
    }
    public List<IProductEntry> GetAll()
    {
        return _entries;
    }
}