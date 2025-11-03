using PgValidator.Configurations;
using PgValidator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgValidator.Validators;

[Validator("E102")]
public class DataTypeValidator : ValidatorBase<EmptyValidationConfig>
{
    protected override void ValidateCore(object value, PgColumn column, ValidationResult result)
    {
        try
        {
            Convert.ChangeType(value, column.Type);
        }
        catch (Exception ex)
        {
            result.AddInternalError(this, ex, column.ColumnName);
        }
    }
}
