using System.Globalization;

namespace ReflectionSerializer.Serialization.Converters;

internal sealed class DoubleConverter : IConcreteConverter
{
    public Type TargetType => typeof(double);
    
    public string Convert(object? value)
    {
        if (value is double doubleValue)
            return doubleValue.ToString(CultureInfo.InvariantCulture);
    
        throw new InvalidOperationException($"Expected double, got {value?.GetType()}");
    }
    
    public object? Convert(string value)
    {
        if (string.IsNullOrEmpty(value))
            return null;

        if (value == "null")
            return 0;

        if (double.TryParse(value, out double parsed))
            return parsed;
        
        throw new InvalidOperationException($"Can't convert {value} to double");
    }
}