using PgValidator.Validation.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgValidator.Validation.Validator;

public class NotNullValidator : ValidatorBase
{
    public NotNullValidator(IValidationConfig config) : base(config)
    {
    }

    public override ValidationResultItem Validate(PgColumn column, object? value)
    {
        if(!column.IsNullable && value == null)
        {
            return ValidationResultItem.Fail(column.ColumnName, this.ErrorCode, $"regex not matched.");
        }
        return ValidationResultItem.Success;
    }
}
