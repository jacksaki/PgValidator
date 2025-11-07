namespace PgValidator.Validation.Config;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ValidationConfigAttribute(Type validatorType, bool targetAllColumns = false) : Attribute
{
    public Type ValidatorType => validatorType;
    public bool TargetAllColumns => targetAllColumns;
}