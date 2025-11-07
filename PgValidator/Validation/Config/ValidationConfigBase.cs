using System.Reflection;

namespace PgValidator.Validation.Config;

public class ValidationConfigBase : IValidationConfig
{
    public bool TargetAllColumns { get; }
    protected ValidationConfigBase()
    {
        var attr = this.GetType().GetCustomAttribute<ValidationConfigAttribute>()!;
        this.TargetAllColumns = attr.TargetAllColumns;
    }
}
