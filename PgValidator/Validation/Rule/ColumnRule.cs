using PgValidator.Validation.Config;
using PgValidator.Validation.Validator;
using System.Text.Json.Serialization;

namespace PgValidator.Validation.Rule;

public class ColumnRule
{
    [JsonPropertyName("column_name")]
    [JsonInclude]
    public string ColumnName { get; private set; } = null!;

    [JsonPropertyName("request_name")]
    [JsonInclude]
    public string RequestName { get; private set; } = null!;

    [JsonPropertyName("validators")]
    [JsonInclude]
    [JsonConverter(typeof(ValidationConfigListConverter))]
    private List<IValidationConfig> Validators { get; set; } = Array.Empty<IValidationConfig>().ToList();

    public List<IValidator> GetValidators()
    {
        if (_cachedValidators != null)
        {
            return _cachedValidators;
        }

        _cachedValidators = ValidatorFactory.CreateAll(this.Validators);
        return _cachedValidators;
    }

    private List<IValidator>? _cachedValidators;
}
