using PgValidator.IO;

namespace PgValidator.Csv;

public class LocalCsvWriter : ICsvWriter<LocalPath>
{
    public async Task SaveToCsvAsync(LocalPath config, string json)
    {
        await File.WriteAllTextAsync(config.Path, JsonToCsvConverter.ConvertToCsv(json));
    }
}