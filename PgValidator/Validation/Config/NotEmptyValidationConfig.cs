using PgValidator.Validation.Validator;
using System.Text.Json.Serialization;

namespace PgValidator.Validation.Config;

[ValidationConfig(typeof(NotEmptyValidator))]
public class NotEmptyValidationConfig : ValidationConfigBase
{
    [JsonPropertyName("allow_space")]
    [JsonInclude]
    public bool AllowSpace { get; private set; }
}
