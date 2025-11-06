namespace PgValidator.Validation.Config;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ValidationConfigAttribute(Type validatorType) : Attribute
{
    public Type ValidatorType => validatorType;
}