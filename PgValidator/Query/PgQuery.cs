using Npgsql;
using System.Text.RegularExpressions;

namespace PgValidator.Query;

public class PgQuery : IDisposable
{
    private NpgsqlConnection _conn;
    public PgQuery(string connectionString)
    {
        _conn = new NpgsqlConnection(connectionString);
        _conn.Open();
        var searchPath = GetSearchPath(connectionString);
        if (searchPath != null)
        {
            SetSchema(searchPath);
        }
    }
    private void SetSchema(string schema)
    {
        var safeSchema = $"\"{schema.Replace("\"", "\"\"")}\"";
        this.ExecuteNonQuery($"SET search_path TO {schema}", null);
    }

    private static string? GetSearchPath(string connectionString)
    {
        var regex = new Regex(@"[sS]earch[pP]ath=(?<schema>\w+);");
        var match = regex.Match(connectionString);
        return match.Success ? match.Groups[1].Value : null;
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
                foreach (var p in item.Parameters)
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

    public async Task<int> ExecuteNonQueryAsync(string sql, IDictionary<string, object?>? parameters)
    {
        using var cmd = CreateCommand(sql, parameters);
        return await cmd.ExecuteNonQueryAsync();
    }
    public int ExecuteNonQuery(string sql, IDictionary<string, object?>? parameters)
    {
        using var cmd = CreateCommand(sql, parameters);
        return cmd.ExecuteNonQuery();
    }

    public async Task<T?> ExecuteScalarAsync<T>(string sql, IDictionary<string, object?>? parameters)
    {
        using var cmd = CreateCommand(sql, parameters);
        var result = await cmd.ExecuteScalarAsync();
        if (result == DBNull.Value)
        {
            return default(T?);
        }
        return (T?)result;
    }

    public T? ExecuteScalar<T>(string sql, IDictionary<string, object?>? parameters)
    {
        using var cmd = CreateCommand(sql, parameters);
        var result = cmd.ExecuteScalar();
        if (result == DBNull.Value)
        {
            return default(T?);
        }
        return (T?)result;
    }

    public async Task<PgStream> SelectAsync(string sql, IDictionary<string, object?>? parameters, CancellationToken ct = default)
    {
        using var cmd = CreateCommand(sql, parameters);
        return new PgStream(await cmd.ExecuteReaderAsync(ct));
    }

    public async Task<PgQueryResult> GetQueryResultAsync(string sql, IDictionary<string, object?> parameters, CancellationToken ct = default)
    {
        var result = await SelectAsync(sql, parameters, ct);

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
