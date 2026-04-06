using System.Reflection;

namespace ReflectionSerializer.Serialization.Converters;

internal sealed class ConvertersRegistry : IConvertersRegistry
{
    private static readonly IConvertersRegistry Instance = new ConvertersRegistry();
    private readonly Dictionary<Type, object> _converters  = new Dictionary<Type, object>();

    private ConvertersRegistry()
    {
        RegisterDefaultConverters();
    }

    private void RegisterDefaultConverters()
    {
        var converterTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => 
                type.IsClass 
                && !type.IsAbstract 
                && !type.IsGenericTypeDefinition
                && typeof(IConcreteConverter).IsAssignableFrom(type));

        foreach (var type in converterTypes)
        {
            var concreteConverter = (IConcreteConverter)Activator.CreateInstance(type)!;
            AddConverter(concreteConverter);
        }
    }

    public static IConvertersRegistry GetInstance()
    {
        return Instance;
    }

    public void AddConverter(IConcreteConverter concreteConverter)
    {
        if (concreteConverter is null)
            throw new NullReferenceException("concreteConverter can't be null");
            
        _converters.TryAdd(concreteConverter.TargetType, concreteConverter);
    }

    public bool TryGetConverter(Type type, out IConcreteConverter? concreteConverter)
    {
        if (type.IsEnum && !_converters.ContainsKey(type))
        {
            var concreteType = typeof(EnumConverter<>).MakeGenericType(type);
            var enumConverter = Activator.CreateInstance(concreteType) as IConcreteConverter;
            
            if (enumConverter is not null)
            {
                _converters.TryAdd(type, enumConverter);
            }
        }
        
        if (_converters.TryGetValue(type, out var converter))
        {
            concreteConverter = (IConcreteConverter)converter;
            return true;
        }

        concreteConverter = null;
        return false;
    }
}