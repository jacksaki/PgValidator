using PgValidator.Validation.Config;

namespace PgValidator.Validation.Validator;

public class NotNullValidator : ValidatorBase
{
    public NotNullValidator(IValidationConfig config) : base(config)
    {
    }

    public override ValidationResultItem Validate(PgColumn column, object? value)
    {
        if (!column.IsNullable && value == null)
        {
            return ValidationResultItem.Fail(column.ColumnName, this.ErrorCode, $"{column.ColumnName} must be not null.");
        }

        return ValidationResultItem.Success;
    }
}
