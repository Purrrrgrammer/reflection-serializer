using System.Text;
using ReflectionSerializer.Serialization.Abstractions;
using ReflectionSerializer.Serialization.Converters;

namespace ReflectionSerializer.Serialization;

internal sealed class CsvSerializer(IConverter converter) : ISerializer
{
    public string Serialize<T>(T serializableObject, string separator = ",") where T : new()
    {
        var type = typeof(T);
        var properties = type.GetProperties();
        var names = new string[properties.Length];
        var values = new string[properties.Length];
        int i = 0;
        var builder = new StringBuilder();
        
        foreach (var property in properties)
        {
            var propertyType = property.PropertyType;
            var propertyName = property.Name;
            var propertyValue = type.GetProperty(propertyName)?.GetValue(serializableObject);
            names[i] = propertyName;
            values[i] = converter.Convert(propertyValue, propertyType);
            i++;
        }

        builder.AppendJoin(separator, names);
        builder.AppendLine();
        builder.AppendJoin(separator, values);

        return builder.ToString();
    }
}