namespace ReflectionSerializer.Serialization.Converters;

internal sealed class DateTimeConverter : IConcreteConverter
{
    public Type TargetType => typeof(DateTime);
    
    public string Convert(object? value)
    {
        if (value is null)
            return "null";
        
        if (value is DateTime dateTime)
            return dateTime.ToString();
    
        throw new InvalidOperationException($"Expected DateTime, got {value?.GetType()}");
    }

    public object? Convert(string value)
    {
        if (string.IsNullOrEmpty(value))
            return null;

        if (value == "null")
            return null;

        if (DateTime.TryParse(value, out DateTime parsedDate))
            return parsedDate;
        
        throw new InvalidOperationException($"Can't convert {value} to DateTime");
    }
}