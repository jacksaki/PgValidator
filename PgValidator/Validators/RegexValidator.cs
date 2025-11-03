using PgValidator.Configurations;
using PgValidator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PgValidator.Validators;

[Validator("E105")]
public class RegexValidator : ValidatorBase<RegexValidationConfig>
{
    protected override void ValidateCore(object value, PgColumn column, ValidationResult result)
    {
        try
        {
            if (!Regex.IsMatch(value.ToString()!, Config.Pattern))
            {
                result.AddError(this.ErrorCode, $"{column.ColumnName}: Regex not match.", column.ColumnName);
            }
        }
        catch (Exception ex)
        {
            result.AddInternalError(this, ex, column.ColumnName);
        }
    }
}