namespace PgValidator.Query;

public class PgBatchCommand()
{
    public IEnumerable<PgBatchCommandItem> Items => _list;

    private List<PgBatchCommandItem> _list = new List<PgBatchCommandItem>();
    public void Add(PgBatchCommandItem item)
    {
        _list.Add(item);
    }

    public void AddRange(IEnumerable<PgBatchCommandItem> items)
    {
        _list.AddRange(items);
    }
    public void Clear()
    {
        _list.Clear();
    }
}
