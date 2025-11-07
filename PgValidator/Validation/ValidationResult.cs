using ZLinq;

namespace PgValidator.Validation;

public class ValidationResult
{
    private ValidationResult(int? rowIndex, IEnumerable<ValidationResultItem> errors)
    {
        this.RowIndex = rowIndex;
        this.Errors = errors.ToList();
    }
    public int? RowIndex { get; }
    public bool HasErrors => this.Errors != null && this.Errors.Count > 0;
    public string ErrorMessages
    {
        get
        {
            if (!this.HasErrors)
            {
                return string.Empty;
            }

            return string.Join(",", this.Errors!.AsValueEnumerable().Select(x => $"{x.ErrorCode}: {x.Message}"));
        }
    }
    public List<ValidationResultItem>? Errors { get; }
    public static ValidationResult OK => new ValidationResult(null, Array.Empty<ValidationResultItem>());
    public static ValidationResult Error(int rowIndex, IEnumerable<ValidationResultItem> errors) => new ValidationResult(rowIndex, errors);
}
