using PgValidator.Validation.Config;
using System.Linq.Expressions;
using System.Reflection;

namespace PgValidator.Validation.Validator;

public static class ValidatorFactory
{
    private static readonly Dictionary<Type, Func<IValidationConfig, IValidator>> _cache = new Dictionary<Type, Func<IValidationConfig, IValidator>>();

    public static IValidator Create(IValidationConfig config)
    {
        var configType = config.GetType();

        if (!_cache.TryGetValue(configType, out var creator))
        {
            var attr = configType.GetCustomAttribute<ValidationConfigAttribute>();
            if (attr == null)
            {
                throw new InvalidOperationException($"ValidationConfigAttribute が設定されていません: {configType.Name}");
            }

            var validatorType = attr.ValidatorType;
            var ctor = validatorType.GetConstructor(new[] { configType });
            if (ctor == null)
            {
                throw new InvalidOperationException($"Validator '{validatorType.Name}' に '{configType.Name}' を受け取るコンストラクタがありません。");
            }

            var param = Expression.Parameter(typeof(IValidationConfig), "cfg");
            var body = Expression.New(ctor, Expression.Convert(param, configType));
            var lambda = Expression.Lambda<Func<IValidationConfig, IValidator>>(body, param);
            creator = lambda.Compile();

            _cache[configType] = creator;
        }

        return creator(config);
    }
}
