namespace PgValidator;

public class PgQueryResult
{
    public PgQueryResultColumnCollection Columns { get; }
    public List<PgQueryResultRow> Rows { get; }
    public PgQueryResult(PgQueryResultColumnCollection columns, List<PgQueryResultRow> rows)
    {
        this.Columns = columns;
        this.Rows = rows;
    }
}
