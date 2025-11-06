namespace PgValidator.Query;

public class PgQueryResult
{
    public PgQueryResultColumnCollection Columns { get; }
    public List<PgQueryResultRow> Rows { get; }
    public PgQueryResult(PgQueryResultColumnCollection columns, List<PgQueryResultRow> rows)
    {
        Columns = columns;
        Rows = rows;
    }
}
