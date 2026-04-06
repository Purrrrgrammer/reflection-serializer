namespace ReflectionSerializer.Serialization.Converters;

internal sealed class IntConverter : IConcreteConverter
{
    public Type TargetType => typeof(int);
    
    public string Convert(object? value)
    {
        if (value is null)
            return "null"; 
        
        if (value is int intValue)
            return intValue.ToString();
    
        throw new InvalidOperationException($"Expected int, got {value?.GetType()}");
    }
    
    public object? Convert(string value)
    {
        if (string.IsNullOrEmpty(value))
            return null;

        if (value == "null")
            return 0;

        if (int.TryParse(value, out int parsed))
            return parsed;
        
        throw new InvalidOperationException($"Can't convert {value} to int");
    }
}