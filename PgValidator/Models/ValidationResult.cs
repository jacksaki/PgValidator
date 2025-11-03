namespace PgValidator.Models;

public class ValidationResult
{
    private readonly List<ValidationError> _errors = new List<ValidationError>();
    public IReadOnlyList<ValidationError> Errors => _errors;

    public bool HasError => _errors.Count > 0;

    public void AddInternalError(object sender, Exception ex, string columnName)
    {
        _errors.Add(new ValidationError("E001", $"{sender.GetType().Name} {ex.Message}", columnName));
    }

    public void AddError(string errorCode, string message, string columnName)
    {
        _errors.Add(new ValidationError(errorCode, message, columnName));
    }
}
