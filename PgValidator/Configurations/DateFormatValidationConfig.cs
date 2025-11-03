using PgValidator.Models;
using PgValidator.Validators;

namespace PgValidator.Configurations;

[ValidatorType(typeof(DateFormatValidator),"DateFormat")]
public class DateFormatValidationConfig : IValidationConfig
{
    public string Format { get; private set; } = "yyyyMMdd";
}
