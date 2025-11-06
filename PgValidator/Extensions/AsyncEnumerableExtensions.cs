namespace PgValidator.Extensions;

public static class AsyncEnumerableExtensions
{
    public static async Task<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> source, CancellationToken ct = default)
    {
        var list = new List<T>();
        await foreach (var item in source.WithCancellation(ct).ConfigureAwait(false))
            list.Add(item);
        return list;
    }

    public static async Task<T[]> ToArrayAsync<T>(this IAsyncEnumerable<T> source, CancellationToken ct = default)
    {
        var list = await source.ToListAsync(ct).ConfigureAwait(false);
        return list.ToArray();
    }
}
