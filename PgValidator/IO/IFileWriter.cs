namespace PgValidator.IO;

public interface IFileWriter<TPath> where TPath : IFilePath
{
    public Task WriteAllTextAsync(TPath path, string contents);
    public Task WriteAllLinesAsync(TPath path, IEnumerable<string> lines);
}
