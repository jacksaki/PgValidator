namespace PgValidator.Validation.Validator;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ErrorCodeAttribute : Attribute
{
    public ErrorCodeAttribute(string errorCode, bool isAllColumn)
    {
        this.ErrorCode = errorCode;
        this.IsAllColumn = isAllColumn;
    }

    public ErrorCodeAttribute(string errorCode)
        : this(errorCode, false) { }
    public bool IsAllColumn { get; }
    public string ErrorCode { get; }
}
