using Npgsql;

namespace PgValidator;

public class PgQuery: IDisposable
{
    private NpgsqlConnection _conn;
    public PgQuery(string connectionString)
    {
        _conn = new NpgsqlConnection(connectionString);
        _conn.Open();
    }

    public async Task<NpgsqlTransaction> BeginTransaction()
    {
        return await _conn.BeginTransactionAsync();
    }

    public async Task<int> BulkExecuteAsync(PgBatchCommand batch)
    {
        var cmd = _conn.CreateBatch();
        foreach (var item in batch.Items)
        {
            var c = new NpgsqlBatchCommand(item.SQL);
            if (item.Parameters != null)
            {
                foreach(var p in item.Parameters)
                {
                    c.Parameters.Add(new NpgsqlParameter(p.Key, p.Value));
                }
            }
            cmd.BatchCommands.Add(c);
        }

        return await cmd.ExecuteNonQueryAsync();
    }
    private NpgsqlCommand CreateCommand(string sql, IDictionary<string, object?>? parameters)
    {
        var cmd = _conn.CreateCommand();
        cmd.CommandText = sql;
        if (parameters != null)
        {
            foreach (var p in parameters)
            {
                cmd.Parameters.Add(new NpgsqlParameter(p.Key, p.Value));
            }
        }
        return cmd;
    }

    public async Task<int>ExecuteNonQueryAsync(string sql, IDictionary<string, object?>? parameters)
    {
        using var cmd = CreateCommand(sql, parameters);
        return await cmd.ExecuteNonQueryAsync();
    }

    public async Task<T?> ExecuteScalarAsync<T>(string sql, IDictionary<string, object?>? parameters)
    {
        using var cmd= CreateCommand(sql, parameters);
        return (T?)(await cmd.ExecuteScalarAsync());
    }
    public async Task<PgStream> SelectAsync(string sql, IDictionary<string, object?>? parameters, CancellationToken ct = default)
    {
        using var cmd = this.CreateCommand(sql, parameters);
        return new PgStream(await cmd.ExecuteReaderAsync(ct));
    }
    public async Task<PgQueryResult> GetQueryResultAsync(string sql, IDictionary<string, object?> parameters, CancellationToken ct = default)
    {
        var result = await this.SelectAsync(sql, parameters, ct);

        var rows = new List<PgQueryResultRow>();
        await foreach (var row in result.WithCancellation(ct).ConfigureAwait(false))
        {
            rows.Add(row);
        }

        return new PgQueryResult(result.Columns, rows);
    }

    public void Dispose()
    {
        _conn.Dispose();
    }
}
