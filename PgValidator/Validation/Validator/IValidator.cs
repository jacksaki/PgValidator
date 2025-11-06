namespace PgValidator.Validation.Validator;

public interface IValidator
{
    public string ErrorCode { get; }
    public bool IsAllColumn { get; }
    public ValidationResultItem Validate(PgColumn column, object? value);
}
