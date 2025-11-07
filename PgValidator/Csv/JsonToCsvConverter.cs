using Csv;
using System.Text.Json;
using ZLinq;

namespace PgValidator.Csv;

public static class JsonToCsvConverter
{
    public static string ConvertToCsv(string json)
    {
        var array = JsonSerializer.Deserialize<List<JsonElement>>(json)
                    ?? throw new InvalidOperationException("Invalid JSON array");

        var headers = array.AsValueEnumerable()
            .SelectMany(e => e.EnumerateObject().Select(p => p.Name))
            .Distinct().ToArray();

        var rows = new List<string[]>();
        foreach (var element in array)
        {
            var obj = element
                .EnumerateObject()
                .ToDictionary(p => p.Name, p => p.Value.ToString());
            rows.Add(
                headers
                .AsValueEnumerable()
                .Select(h => obj.TryGetValue(h, out var v) ? v : string.Empty)
                .ToArray());
        }

        return CsvWriter.WriteToText(headers, rows, ',');
    }
}