using PgValidator.Validation.Config;
using System.Reflection;

namespace PgValidator.Validation.Validator;

public abstract class ValidatorBase : IValidator
{
    public string ErrorCode { get; }
    public IValidationConfig Config { get; protected set; }
    public bool TargetAllColumn { get; }
    protected ValidatorBase(IValidationConfig config)
    {
        this.Config = config;
        this.TargetAllColumn = config.TargetAllColumns;
        ErrorCode = this.GetType().GetCustomAttribute<ErrorCodeAttribute>()!.ErrorCode;
    }

    public abstract ValidationResultItem Validate(PgColumn column, object? value);
}
