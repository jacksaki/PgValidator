using PgValidator.Validation.Config;
using System.Globalization;

namespace PgValidator.Validation.Validator;

public class DateFormatValidator : ValidatorBase
{
    public readonly string DateFormat;
    public DateFormatValidator(DateFormatValidationConfig config) : base(config)
    {
        this.DateFormat = config.Format;
    }

    public override ValidationResultItem Validate(PgColumn column, object? value)
    {
        if (column.IsNullable && value == null)
        {
            return ValidationResultItem.Success;
        }

        return DateTime.TryParseExact(value!.ToString(),
            this.DateFormat,
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out var d)
                ? ValidationResultItem.Success :
                ValidationResultItem.Fail(column.ColumnName, this.ErrorCode, $"");

    }
}
