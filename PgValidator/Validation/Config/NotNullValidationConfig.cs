using PgValidator.Validation.Validator;

namespace PgValidator.Validation.Config;

[ValidationConfig(typeof(NotNullValidator), true)]
public class NotNullValidationConfig : ValidationConfigBase
{
}
