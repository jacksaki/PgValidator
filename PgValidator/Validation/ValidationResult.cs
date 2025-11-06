namespace PgValidator.Validation;

public class ValidationResult
{
    private ValidationResult(int? rowIndex, IEnumerable<ValidationResultItem> errors)
    {
        this.RowIndex = rowIndex;
        this.Errors = errors.ToList();
    }
    public int? RowIndex { get; }
    public List<ValidationResultItem>? Errors { get; }
    public static ValidationResult OK => new ValidationResult(null, Array.Empty<ValidationResultItem>());
    public static ValidationResult Error(int rowIndex, IEnumerable<ValidationResultItem> errors) => new ValidationResult(rowIndex, errors);
}
