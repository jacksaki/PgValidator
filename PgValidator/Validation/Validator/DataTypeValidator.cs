using PgValidator.Validation.Config;

namespace PgValidator.Validation.Validator
{
    public class DataTypeValidator : ValidatorBase
    {
        public DataTypeValidator(DataTypeValidationConfig config) : base(config)
        {
        }

        public override ValidationResultItem Validate(PgColumn column, object? value)
        {
            if (column.IsNullable && value == null)
            {
                return ValidationResultItem.Success;
            }

            try
            {
                Convert.ChangeType(value, column.Type);
                return ValidationResultItem.Success;
            }
            catch
            {
                return ValidationResultItem.Fail(column.ColumnName, this.ErrorCode, $"");
            }
        }
    }
}
