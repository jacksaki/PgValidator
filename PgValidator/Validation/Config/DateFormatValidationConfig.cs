using PgValidator.Validation.Validator;
using System.Text.Json.Serialization;

namespace PgValidator.Validation.Config
{
    [ValidationConfig(typeof(DateFormatValidator))]
    public class DateFormatValidationConfig : IValidationConfig
    {
        [JsonPropertyName("format")]
        [JsonInclude]
        public string Format { get; private set; } = null!;
    }
}
