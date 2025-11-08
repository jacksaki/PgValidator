using PgValidator.IO;

namespace PgValidator.Csv;

public class LocalCsvWriter : ICsvWriter<LocalPath>
{
    public async Task SaveToCsvAsync(LocalPath config, string json)
    {
        await System.IO.File.WriteAllTextAsync(config.Path, json);
    }
}