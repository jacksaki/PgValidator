using System.Data.Common;
using ZLinq;
namespace PgValidator.Query;

public sealed class PgStream : IAsyncEnumerable<PgQueryResultRow>
{
    private readonly DbDataReader _reader;
    public PgQueryResultColumnCollection Columns { get; }

    internal PgStream(DbDataReader reader)
    {
        _reader = reader;
        Columns = new PgQueryResultColumnCollection(
            Enumerable.Range(0, reader.FieldCount)
            .AsValueEnumerable()
            .Select(i => new PgQueryResultColumn(i, reader.GetName(i), reader.GetFieldType(i))).ToList());
    }

    public async IAsyncEnumerator<PgQueryResultRow> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        var values = new object[_reader.FieldCount];
        while (await _reader.ReadAsync().ConfigureAwait(false))
        {
            _reader.GetValues(values);
            yield return new PgQueryResultRow(Columns, (object[])values.Clone());
        }
    }
}