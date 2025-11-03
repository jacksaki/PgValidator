using PgValidator.Models;
using PgValidator.Validators;

namespace PgValidator.Services;

public class ValidationRunner
{
    private readonly List<IValidator> _validators = new();

    public void AddValidator(IValidator validator) => _validators.Add(validator);

    public ValidationResult Run(object? value, PgColumn column)
    {
        var result = new ValidationResult();

        foreach (var validator in _validators)
        {
            try
            {
                validator.Validate(value, column, result);
            }
            catch (Exception ex)
            {
                result.AddError(validator.ErrorCode, ex.Message, column.ColumnName);
            }
        }

        return result;
    }
}