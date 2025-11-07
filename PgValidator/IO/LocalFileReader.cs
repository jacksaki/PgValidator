namespace PgValidator.IO;

public class LocalFileReader : IFileReader<LocalPath>
{
    public async IAsyncEnumerable<string> EnumerateLinesAsync(LocalPath path)
    {
        foreach (var line in await System.IO.File.ReadAllLinesAsync(path.Path))
        {
            yield return line;
        }
    }

    public async Task<string[]> ReadAllLinesAsync(LocalPath path)
    {
        return await System.IO.File.ReadAllLinesAsync(path.Path);
    }

    public async Task<string> ReadAllTextAsync(LocalPath path)
    {
        return await System.IO.File.ReadAllTextAsync(path.Path);
    }
}
