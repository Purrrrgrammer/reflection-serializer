namespace ReflectionSerializer.Serialization.Converters;

internal sealed class LongConverter : IConcreteConverter
{
    public Type TargetType => typeof(long);
    
    public string Convert(object? value)
    {
        if (value is long longValue)
            return longValue.ToString();
    
        throw new InvalidOperationException($"Expected long, got {value?.GetType()}");
    }
    
    public object? Convert(string value)
    {
        if (string.IsNullOrEmpty(value))
            return null;

        if (value == "null")
            return 0;

        if (long.TryParse(value, out long parsed))
            return parsed;
        
        throw new InvalidOperationException($"Can't convert {value} to long");
    }
}