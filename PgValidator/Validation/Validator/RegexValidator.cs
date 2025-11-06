using PgValidator.Validation.Config;
using System.Text.RegularExpressions;

namespace PgValidator.Validation.Validator;

public class RegexValidator : ValidatorBase
{
    private readonly Regex _regex;
    public RegexValidator(RegexValidationConfig config) : base(config)
    {
        _regex = new Regex(config.Pattern);
    }
    public override ValidationResultItem Validate(PgColumn column, object? value)
    {
        if (value == null || _regex.IsMatch(value.ToString()!))
        {
            return ValidationResultItem.Success;
        }

        return ValidationResultItem.Fail(column.ColumnName, this.ErrorCode, $"regex not matched.");
    }
}
