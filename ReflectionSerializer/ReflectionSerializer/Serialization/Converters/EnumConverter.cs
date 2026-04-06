namespace ReflectionSerializer.Serialization.Converters;

internal sealed class EnumConverter<T> : IConcreteConverter where T : Enum
{
    public Type TargetType => typeof(T);
    
    public string Convert(object? value)
    {
        if (value is null)
            return "null";
        
        if (value is T enumValue)
            return enumValue.ToString();
        
        throw new InvalidOperationException($"Expected {typeof(T)}, got {value.GetType()}");
    }
    
    public object? Convert(string value)
    {
        if (string.IsNullOrEmpty(value))
            return null;

        if(Enum.TryParse(typeof(T), value, out var enumValue))
            return enumValue;
        
        throw new InvalidOperationException($"Can't convert {typeof(T).Name} to double");
    }
}