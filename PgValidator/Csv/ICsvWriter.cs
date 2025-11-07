using PgValidator.IO;

namespace PgValidator.Csv;

public interface ICsvWriter<TConfig> where TConfig : IFilePath
{
    Task SaveToCsvAsync(TConfig config, string json);
}