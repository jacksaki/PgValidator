using PgValidator.Query;
using System.Text.RegularExpressions;
using System.Threading;

namespace PgValidator;

public class PgTable
{
    private static readonly string TableSQL = @"SELECT
 T.table_schema
,T.table_name
,T.is_insertable_into
FROM
 information_schema.tables T
WHERE
T.table_schema = @table_schema
AND T.table_name = @table_name";

    [DbColumn("table_schema")]
    public string TableSchema { get; private set; } = null!;
    [DbColumn("table_name")]
    public string TableName { get; private set; } = null!;
    [DbColumn("is_insertable_into")]
    public bool IsInsertable { get; private set; }
    public PgColumnCollection Columns { get; private set; } = null!;

    public static async Task<PgTable> LoadAsync(string connectionString, string tableName)
    {
        using var q = new PgQuery(connectionString);
        var schema = await q.ExecuteScalarAsync<string>("SELECT CURRENT_SCHEMA()", null);
        return await LoadAsync(connectionString, schema!, tableName);
    }

    public static async Task<PgTable> LoadAsync(string connectionString, string tableSchema, string tableName)
    {
        using var q = new PgQuery(connectionString);
        var p = new Dictionary<string, object?>
        {
                { "table_schema",tableSchema },
                {"table_name",tableName }
        };
        var result = await q.GetQueryResultAsync(TableSQL, p).ConfigureAwait(false);
        var table = result.Rows.First().Create<PgTable>();
        table.Columns = await PgColumnCollection.LoadAsync(connectionString, table);
        return table;
    }
}
