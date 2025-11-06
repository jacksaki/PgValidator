using System.Collections;

namespace PgValidator.Query;

public sealed class PgQueryResultColumnCollection : IEnumerable<PgQueryResultColumn>
{
    private readonly List<PgQueryResultColumn> _columns;

    internal PgQueryResultColumnCollection(List<PgQueryResultColumn> columns)
    {
        _columns = columns;
    }

    public PgQueryResultColumn this[int index] => _columns.Where(x => x.Index == index).First();
    public PgQueryResultColumn this[string name] => _columns.Where(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).First();

    public IEnumerator<PgQueryResultColumn> GetEnumerator()
    {
        return ((IEnumerable<PgQueryResultColumn>)_columns).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_columns).GetEnumerator();
    }
}
