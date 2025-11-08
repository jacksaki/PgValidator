using PgValidator.Validation.Config;
using System.Text.RegularExpressions;

namespace PgValidator.Validation.Validator;

[ErrorCode("E005")]
public class RegexValidator : ValidatorBase
{
    public string Patten { get; }
    private readonly Regex _regex;
    public RegexValidator(RegexValidationConfig config) : base(config)
    {
        this.Patten = config.Pattern;
        _regex = new Regex(config.Pattern ?? string.Empty);
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
