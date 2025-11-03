using System.Collections;
using System.Diagnostics;

namespace PgValidator;

public class PgColumnCollection:IEnumerable<PgColumn>
{
    private static readonly string ColumnsSQL = @"SELECT
 C.column_name
,C.ordinal_position
,C.is_nullable
,C.data_type
,C.character_maximum_length
,C.numeric_precision
,C.numeric_scale
,C.datetime_precision
,C.is_updatable
,C.column_default::text AS column_default
FROM
 information_schema.tables T
INNER JOIN information_schema.columns C ON (T.table_schema = C.table_schema AND T.table_name = C.table_name)
WHERE
T.table_schema = @table_schema
AND T.table_name = @table_name
ORDER BY
 C.ordinal_position";

    public PgTable Parent { get; }
    private PgColumnCollection(PgTable parent)
    {
        this.Parent = parent;
    }

    public PgColumn this[int index] => _columns[index];

    public PgColumn this[string columnName] => _columns.Where(x=>x.ColumnName.Equals(columnName,StringComparison.OrdinalIgnoreCase)).First();

    private List<PgColumn> _columns { get; set; } = null!;

    internal static async Task<PgColumnCollection> LoadAsync(string connectionString, PgTable parent)
    {
        using var q = new PgQuery(connectionString);
        var result = await q.GetQueryResultAsync(ColumnsSQL, new Dictionary<string, object?>
    {
        {"table_schema",parent.TableSchema },
        {"table_name",parent.TableName }
    });
        var columns = new PgColumnCollection(parent);
        columns._columns = result.Rows.Select(x => x.Create<PgColumn, PgColumnCollection>(columns)).ToList();
        return columns;
    }

    public IEnumerator<PgColumn> GetEnumerator()
    {
        return ((IEnumerable<PgColumn>)_columns).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_columns).GetEnumerator();
    }
}
