using PgValidator.Validation.Config;
using System.Buffers.Text;

namespace PgValidator.Validation.Validator;

[ErrorCode("E001")]
public class DataTypeValidator : ValidatorBase
{
    public DataTypeValidator(DataTypeValidationConfig config) : base(config)
    {
    }

    private bool IsCompatible(object? value, Type targetType)
    {
        if (value == null)
        {
            return !targetType.IsValueType || Nullable.GetUnderlyingType(targetType) != null;
        }

        var valueType = value.GetType();

        if (targetType == typeof(byte[]))
        {
            Convert.FromBase64String(value.ToString()!);
            return true;
        }

        if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            targetType = Nullable.GetUnderlyingType(targetType)!;
        }

        if (targetType.IsAssignableFrom(valueType))
        {
            return true;
        }

        try
        {
            Convert.ChangeType(value, targetType);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public override ValidationResultItem Validate(PgColumn column, object? value)
    {
        if (column.IsNullable && value == null)
        {
            return ValidationResultItem.Success;
        }

        if (IsCompatible(value, column.Type))
        {
            return ValidationResultItem.Success;
        }
        else
        {
            return ValidationResultItem.Fail(column.ColumnName, this.ErrorCode, $"");
        }
    }
}