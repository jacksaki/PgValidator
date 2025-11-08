using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace PgValidator.Validation;

public static class JsonElementExtension
{
    public static object? GetValue(this JsonNode? node)
    {
        if(node == null)
        {
            return null;
        }
        var valueNode = node as JsonValue;

        switch (valueNode?.GetValue<JsonElement>().ValueKind)
        {
            case JsonValueKind.Number:
                var element = valueNode.GetValue<JsonElement>();

                if(element.TryGetInt32(out int int32Value))
                {
                    return int32Value;
                }
                if(element.TryGetInt64(out long int64Value))
                {
                    return int64Value;
                }
                if(element.TryGetUInt32(out uint uint32Value))
                {
                    return uint32Value;
                }
                if(element.TryGetUInt64(out ulong ulongValue))
                {
                    return ulongValue;
                }
                if(element.TryGetInt16(out short shortValue))
                {
                    return shortValue;
                }
                if(element.TryGetUInt16(out ushort ushortValue))
                {
                    return ushortValue;
                }
                if(element.TryGetDouble(out double doubleValue))
                {
                    return doubleValue;
                }
                if (element.TryGetSingle(out float floatValue))
                {
                    return floatValue;
                }
                else
                {
                    return element.GetDecimal();
                }
            case JsonValueKind.String:
                return valueNode.GetValue<JsonElement>().GetString();
            case JsonValueKind.True:
            case JsonValueKind.False:
                return valueNode.GetValue<JsonElement>().GetBoolean();
            case JsonValueKind.Null:
            case JsonValueKind.Undefined:
                return null;
            default:
                return null;
        }
    }
}
