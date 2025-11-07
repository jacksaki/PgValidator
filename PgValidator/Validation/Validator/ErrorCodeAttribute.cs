namespace PgValidator.Validation.Validator;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ErrorCodeAttribute : Attribute
{
    public ErrorCodeAttribute(string errorCode)
    {
        this.ErrorCode = errorCode;
    }

    public string ErrorCode { get; }
}
