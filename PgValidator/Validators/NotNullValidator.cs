using PgValidator.Configurations;
using PgValidator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgValidator.Validators;

[Validator("E101")]
public class NotNullValidator : ValidatorBase<EmptyValidationConfig>
{
    protected override void ValidateCore(object value, PgColumn column, ValidationResult result)
    {
        if (!column.IsNullable && value == null)
        {
            result.AddError(this.ErrorCode, $"{column.ColumnName} null value is not allowd.", column.ColumnName);
        }
    }
}
