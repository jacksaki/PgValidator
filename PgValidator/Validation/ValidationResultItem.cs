namespace PgValidator.Validation;

public class ValidationResultItem(bool isValid, string? columnName = null, string? errorCode = null, string? message = null)
{
    public bool IsValid => isValid;
    public string? ColumnName => columnName;
    public string? ErrorCode => errorCode;
    public string? Message => message;
    public static ValidationResultItem Success => new ValidationResultItem(true);
    public static ValidationResultItem Fail(string columnName, string code, string msg) => new ValidationResultItem(false, columnName, code, msg);
}
