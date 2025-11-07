namespace PgValidator.RequestMapping;

public class LocalRequestMappingJsonLoader(string path) : IRequestMappingJsonLoader
{
    public string Path => path;
    public async Task<string> LoadJsonAsync()
    {
        return await System.IO.File.ReadAllTextAsync(this.Path);
    }
}
