using System.Globalization;

namespace ReflectionSerializer.Serialization.Converters;

internal sealed class FloatConverter : IConcreteConverter
{
    public Type TargetType => typeof(float);
    
    public string Convert(object? value)
    {
        if (value is float floatValue)
            return floatValue.ToString(CultureInfo.InvariantCulture);
    
        throw new InvalidOperationException($"Expected float, got {value.GetType()}");
    }
    
    public object? Convert(string value)
    {
        if (string.IsNullOrEmpty(value))
            return null;

        if (value == "null")
            return 0;

        if (float.TryParse(value, out float parsed))
            return parsed;
        
        throw new InvalidOperationException($"Can't convert {value} to float");
    }
}