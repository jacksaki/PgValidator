using PgValidator.Configurations;
using PgValidator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgValidator.Validators
{
    [Validator("E103")]
    public class NotEmptyValidator : ValidatorBase<EmptyValidationConfig>
    {
        protected override void ValidateCore(object? value, PgColumn column, ValidationResult result)
        {
            if (string.IsNullOrEmpty(value?.ToString()))
            {
                result.AddError(this.ErrorCode, $"Empty value not allowed.", column.ColumnName);
            }
        }
    }
}
