using PgValidator.Configurations;
using PgValidator.JsonConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PgValidator.Models
{
    public class ColumnRule
    {
        [JsonPropertyName("name")]
        [JsonInclude]
        public string Name { get; private set; } = string.Empty;

        [JsonPropertyName("validations")]
        [JsonConverter(typeof(ValidationConfigListConverter))]
        [JsonInclude]
        public List<IValidationConfig> Validations { get; private set; } = new();
    }
}
