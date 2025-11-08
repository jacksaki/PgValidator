using PgValidator.Validation.Config;
using System.Linq.Expressions;
using System.Reflection;
using ZLinq;

namespace PgValidator.Validation.Validator;

public static class ValidatorFactory
{
    static ValidatorFactory()
    {
        _targetAllColumnsConfigCache = Assembly.GetExecutingAssembly().GetTypes()
            .AsValueEnumerable()
            .Where(x =>
                x.IsClass && 
                !x.IsAbstract && 
                typeof(IValidationConfig).IsAssignableFrom(x) &&
                x.GetCustomAttribute<ValidationConfigAttribute>()?.TargetAllColumns == true
            )
            .Select(x => Create(CreateConfigInstance(x))).ToList();
    }

    private static List<IValidator> _targetAllColumnsConfigCache { get; }
    private static readonly Dictionary<Type, Func<IValidationConfig, IValidator>> _cache = new Dictionary<Type, Func<IValidationConfig, IValidator>>();

    private static IValidationConfig CreateConfigInstance(Type t)
    {
        var ctor = t.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, Type.EmptyTypes)!;
        var body = Expression.New(ctor);
        var lambda = Expression.Lambda<Func<IValidationConfig>>(body);
        return lambda.Compile()();
    }

    private static Func<IValidationConfig, IValidator> CreateConstructor(Type configType)
    {
        var attr = configType.GetCustomAttribute<ValidationConfigAttribute>();
        if (attr == null)
        {
            throw new InvalidOperationException($"ValidationConfigAttribute が設定されていません: {configType.Name}");
        }

        var validatorType = attr.ValidatorType;
        var ctor = validatorType
            .GetConstructors()
            .FirstOrDefault(c =>
            {
                var p = c.GetParameters();
                return p.Length == 1 && p[0].ParameterType.IsAssignableFrom(configType);
            });
        if (ctor == null)
        {
            throw new InvalidOperationException($"Validator '{validatorType.Name}' に '{configType.Name}' を受け取るコンストラクタがありません。");
        }

        var param = Expression.Parameter(typeof(IValidationConfig), "config");
        var body = Expression.New(ctor, Expression.Convert(param, configType));
        var lambda = Expression.Lambda<Func<IValidationConfig, IValidator>>(body, param);
        return lambda.Compile();
    }

    public static List<IValidator> CreateAll(IEnumerable<IValidationConfig> configurations)
    {
        return _targetAllColumnsConfigCache.Union(configurations.Select(x => Create(x))).ToList();
    }

    public static IValidator Create(IValidationConfig config)
    {
        var configType = config.GetType();

        if (!_cache.TryGetValue(configType, out var creator))
        {
            creator = CreateConstructor(configType);
            _cache[configType] = creator;
        }

        return creator(config);
    }
}
