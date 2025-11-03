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
    public class ValidationConfigListConverter : JsonConverter<List<IValidationConfig>>
    {
        public override List<IValidationConfig>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var list = new List<IValidationConfig>();
            if (reader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            var conv = new ValidationConfigConverter();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                    return list;

                var item = conv.Read(ref reader, typeof(IValidationConfig), options);
                if (item != null) list.Add(item);
            }

            throw new JsonException("JSON配列のパースに失敗しました。");
        }

        public override void Write(Utf8JsonWriter writer, List<IValidationConfig> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach (var item in value)
            {
                JsonSerializer.Serialize(writer, item, item.GetType(), options);
            }
            writer.WriteEndArray();
        }
    }
}
