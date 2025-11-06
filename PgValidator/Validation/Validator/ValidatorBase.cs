using PgValidator.Validation.Config;
using System.Reflection;

namespace PgValidator.Validation.Validator;

public abstract class ValidatorBase : IValidator
{
    public string ErrorCode { get; }
    public IValidationConfig Config { get; protected set; }
    public bool IsAllColumn { get; }
    protected ValidatorBase(IValidationConfig config)
    {
        this.Config = config;
        var attr = this.GetType().GetCustomAttribute<ErrorCodeAttribute>();
        this.IsAllColumn = attr?.IsAllColumn == true ? true : false;
        ErrorCode = attr?.ErrorCode ?? "UNKNOWN";
    }

    public abstract ValidationResultItem Validate(PgColumn column, object? value);
}
