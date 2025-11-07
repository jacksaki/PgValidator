using PgValidator.Validation.Validator;
using System.Text.Json.Serialization;

namespace PgValidator.Validation.Config
{
    [ValidationConfig(typeof(DateFormatValidator))]
    public class DateFormatValidationConfig : ValidationConfigBase
    {
        [JsonPropertyName("format")]
        [JsonInclude]
        public string Format { get; private set; } = null!;
    }
}
