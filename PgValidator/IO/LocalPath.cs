namespace PgValidator.IO;

public class LocalPath(string path) : IFilePath
{
    public string Path => path;
}
