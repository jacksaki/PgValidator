using PgValidator.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PgValidator.JsonConverters
{
    public class ValidationConfigConverter : JsonConverter<IValidationConfig>
    {
        public override IValidationConfig? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var doc = JsonDocument.ParseValue(ref reader);
            var root = doc.RootElement;

            if (!root.TryGetProperty("type", out var typeProp))
                throw new JsonException("type property not found in validation");

            var type = typeProp.GetString();

            return type switch
            {
                "Regex" => JsonSerializer.Deserialize<RegexValidationConfig>(root.GetRawText(), options),
                "Required" => JsonSerializer.Deserialize<NotEmptyValidationConfig>(root.GetRawText(), options),
                "DateFormat" => JsonSerializer.Deserialize<DateFormatValidationConfig>(root.GetRawText(), options),
                _ => throw new NotSupportedException($"不明なValidatorタイプ: {type}")
            };
        }

        public override void Write(Utf8JsonWriter writer, IValidationConfig value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}
