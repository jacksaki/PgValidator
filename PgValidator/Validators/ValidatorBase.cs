using PgValidator.Configurations;
using PgValidator.Models;

namespace PgValidator.Validators
{
    public abstract class ValidatorBase<TConfig> : IValidator
        where TConfig : IValidationConfig, new()
    {
        protected TConfig Config { get; }
        public string ErrorCode { get; }
        
        protected ValidatorBase() : this(new TConfig())
        {
        }

        protected ValidatorBase(TConfig? config)
        {
            var type = this.GetType();
            var attr = (ValidatorAttribute?)Attribute.GetCustomAttribute(type, typeof(ValidatorAttribute))!;
            this.ErrorCode = attr.ErrorCode;
            Config = config ?? new TConfig();
        }

        public void Validate(object? value, PgColumn column, ValidationResult result)
        {
            if (value is null)
            {
                return ;
            }

            ValidateCore(value, column, result);
        }

        protected abstract void ValidateCore(object value, PgColumn column, ValidationResult result);
    }
}
