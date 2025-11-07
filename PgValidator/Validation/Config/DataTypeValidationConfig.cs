using PgValidator.Validation.Validator;

namespace PgValidator.Validation.Config;

[ValidationConfig(typeof(DataTypeValidator), true)]
public class DataTypeValidationConfig : ValidationConfigBase
{
}