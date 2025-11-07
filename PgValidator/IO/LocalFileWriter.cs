namespace PgValidator.IO;

public class LocalFileWriter : IFileWriter<LocalPath>
{
    public async Task WriteAllTextAsync(LocalPath path, string contents)
    {
        await System.IO.File.WriteAllTextAsync(path.Path, contents);
    }

    public async Task WriteAllLinesAsync(LocalPath path, IEnumerable<string> lines)
    {
        await System.IO.File.WriteAllLinesAsync(path.Path, lines);
    }
}
