using PgValidator.Configurations;
using PgValidator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgValidator.Validators;

[Validator("E104")]
public class DateFormatValidator : ValidatorBase<DateFormatValidationConfig>
{
    protected override void ValidateCore(object value, PgColumn column, ValidationResult result)
    {
		try
		{
            if(!DateTime.TryParseExact(value.ToString(), this.Config.Format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var d))
            {
                result.AddError(this.ErrorCode, $"Date convert failed.{value}(format: {this.Config.Format}", column.ColumnName);
            }
        }
        catch (Exception ex)
		{
            result.AddInternalError(this,ex, column.ColumnName);    
		}
    }
}
