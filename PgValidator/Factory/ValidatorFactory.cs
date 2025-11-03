using PgValidator.Configurations;
using PgValidator.Models;
using PgValidator.Validators;
using System.Reflection;

namespace PgValidator.Factory;

public static class ValidatorFactory
{
    private static readonly Dictionary<Type, Type> _map = new();

    static ValidatorFactory()
    {
        var configTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => t.GetCustomAttribute<ValidatorTypeAttribute>() != null);

        foreach (var configType in configTypes)
        {
            var attr = configType.GetCustomAttribute<ValidatorTypeAttribute>()!;
            _map[configType] = attr.Type;
        }
    }

    public static IValidator Create(IValidationConfig config)
    {
        var configType = config.GetType();

        if (!_map.TryGetValue(configType, out var validatorType))
        {
            throw new InvalidOperationException($"Validator not found: {configType.Name}");
        }

        return (IValidator)Activator.CreateInstance(validatorType, config)!;
    }

    public static IValidator Create<TValidator>() where TValidator : IValidator, new()
    {
        return new TValidator();
    }
}
