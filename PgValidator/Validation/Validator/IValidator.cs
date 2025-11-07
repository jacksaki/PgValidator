namespace PgValidator.Validation.Validator;

public interface IValidator
{
    public string ErrorCode { get; }
    public bool TargetAllColumn { get; }
    public ValidationResultItem Validate(PgColumn column, object? value);
}
