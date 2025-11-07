namespace PgValidator.IO;

public interface IFileReader<TPath> where TPath : IFilePath
{
    public Task<string> ReadAllTextAsync(TPath path);
    public IAsyncEnumerable<string> EnumerateLinesAsync(TPath path);
    public Task<string[]> ReadAllLinesAsync(TPath path);
}