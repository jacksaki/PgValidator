using PgValidator.Validation.Config;

namespace PgValidator.Validation.Validator;

[ErrorCode("E003")]
public class NotEmptyValidator : ValidatorBase
{
    private readonly bool AllowSpace;
    public NotEmptyValidator(NotEmptyValidationConfig config) : base(config)
    {
        this.AllowSpace = config.AllowSpace;
    }

    public override ValidationResultItem Validate(PgColumn column, object? value)
    {
        if (this.AllowSpace)
        {
            if (string.IsNullOrEmpty(value?.ToString()))
            {
                return ValidationResultItem.Fail(column.ColumnName, this.ErrorCode, $"{column.ColumnName} must be not empty");
            }
        }
        else
        {
            if (string.IsNullOrWhiteSpace(value?.ToString()))
            {
                return ValidationResultItem.Fail(column.ColumnName, this.ErrorCode, $"{column.ColumnName} must be not empty");
            }
        }

        return ValidationResultItem.Success;
    }
}
