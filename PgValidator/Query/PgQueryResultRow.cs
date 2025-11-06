namespace PgValidator.Query;

public sealed class PgQueryResultRow
{
    internal PgQueryResultRow(PgQueryResultColumnCollection columns, object[] values)
    {
        Columns = columns;
        Values = values;
    }

    internal PgQueryResultColumnCollection Columns { get; }
    internal object[] Values { get; }
    public object? this[string name] => this[Columns[name].Index];
    public object? this[int index]
    {
        get
        {
            var value = Values[index];
            return value == DBNull.Value ? null : value;
        }
    }
}
