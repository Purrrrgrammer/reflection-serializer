namespace ReflectionSerializer.Serialization.Converters;

internal sealed class StringConverter : IConcreteConverter
{
    public Type TargetType => typeof(string);
    
    public string Convert(object? value)
    {
        if (value is null)
            return "null";
        
        if (value is string strValue)
        {
            bool needsQuoting = strValue.Contains(',') 
                                || strValue.Contains('"') 
                                || strValue.Contains('\n') 
                                || strValue.Contains('\r');
            
            if (needsQuoting)
            {
                strValue = strValue.Replace("\"", "\"\"");
                return $"\"{strValue}\"";
            }
            
            return strValue;
        }
    
        throw new InvalidOperationException($"Expected string, got {value.GetType()}");
    }
    
    public object? Convert(string value)
    {
        if (string.IsNullOrEmpty(value) || value == "null")
            return null;
        
        if (value.StartsWith('"') && value.EndsWith('"') && value.Length >= 2)
        {
            var unquoted = value.Substring(1, value.Length - 2);
            unquoted = unquoted.Replace("\"\"", "\"");
            return unquoted;
        }
        
        return value;
    }
}