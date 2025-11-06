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
    public List<IValidationConfig> Validators { get; private set; } = null!;

    private List<IValidator>? _cachedValidators;
    public IReadOnlyList<IValidator> GetValidators()
    {
        if (_cachedValidators != null)
        {
            return _cachedValidators;
        }

        _cachedValidators = Validators
            .Select(conf => ValidatorFactory.Create(conf))
            .ToList();

        return _cachedValidators;
    }
}
