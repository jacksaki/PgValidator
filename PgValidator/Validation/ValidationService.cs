using PgValidator.Validation.Rule;
using System.Text.Json.Nodes;

namespace PgValidator.Validation;

public class ValidationService
{
    public static async Task<ValidationResult> ValidateAllAsync(string connectionString, ValidationRuleSet ruleSet, string json)
    {
        var root = JsonNode.Parse(json);
        if (root == null)
        {
            throw new ArgumentException("Invalid JSON.");
        }

        var records = root["records"]?.AsArray();
        if (records == null)
        {
            throw new ArgumentException("records is null");
        }

        var table = await PgTable.LoadAsync(connectionString, ruleSet.TableName);

        int index = 0;
        var errors = new List<ValidationResultItem>();

        foreach (var recordNode in records)
        {
            var obj = recordNode!.AsObject();
            var dict = obj.ToDictionary(x => x.Key, y => (object?)y.Value);

            foreach (var column in ruleSet.Columns)
            {
                var col = table.Columns[column.ColumnName];
                dict.TryGetValue(column.RequestName, out var value);

                foreach (var v in column.GetValidators())
                {
                    var result = v.Validate(col, value);
                    if (!result.IsValid)
                    {
                        errors.Add(new ValidationResultItem(false, result.ErrorCode, result.Message));
                    }
                }
            }
            index++;
            if (errors.Count > 0)
            {
                return ValidationResult.Error(index, errors);
            }
        }

        return ValidationResult.OK;
    }
}
