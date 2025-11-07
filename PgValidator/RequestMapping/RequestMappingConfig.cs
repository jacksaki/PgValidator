using System.Text.Json;
using System.Text.Json.Serialization;

namespace PgValidator.RequestMapping;

public class RequestMappingConfig
{
    public static async Task<RequestMappingConfig[]> LoadAsync(IRequestMappingJsonLoader loader)
    {
        var json = await loader.LoadJsonAsync();
        return JsonSerializer.Deserialize<RequestMappingConfig[]>(json)!;
    }

    [JsonPropertyName("company")]
    [JsonInclude]
    public string Company { get; private set; } = null!;
    [JsonPropertyName("request_name")]
    [JsonInclude]
    public string RequestName { get; private set; } = null!;
    [JsonPropertyName("table_mapping_json_path")]
    [JsonInclude]
    public string TableMappingJsonPath { get; private set; } = null!;
    [JsonPropertyName("table_mapping_json_bucket")]
    [JsonInclude]
    public string TableMappingJsonBucket { get; private set; } = null!;
    [JsonPropertyName("table_mapping_json_key")]
    [JsonInclude]
    public string TableMappingJsonKey { get; private set; } = null!;
}
