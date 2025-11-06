using System.Text.Json.Serialization;

namespace PgValidator.Validation.Rule;

public class ValidationRuleSet
{
    [JsonPropertyName("table_name")]
    [JsonInclude]
    public string TableName { get; private set; } = null!;
    [JsonPropertyName("columns")]
    [JsonInclude]
    public List<ColumnRule> Columns { get; private set; } = null!;
}
