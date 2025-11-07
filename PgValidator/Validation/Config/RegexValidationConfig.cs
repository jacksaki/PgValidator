using PgValidator.Validation.Validator;
using System.Text.Json.Serialization;

namespace PgValidator.Validation.Config;

[ValidationConfig(typeof(RegexValidator))]
public class RegexValidationConfig : ValidationConfigBase
{
    [JsonPropertyName("pattern")]
    [JsonInclude]
    public string Pattern { get; private set; } = null!;
}