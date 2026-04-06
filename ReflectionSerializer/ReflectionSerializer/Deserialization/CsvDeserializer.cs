using ReflectionSerializer.Deserialization.Abstractions;
using ReflectionSerializer.Serialization.Converters;

namespace ReflectionSerializer.Deserialization;

internal sealed class CsvDeserializer(IConverter converter) : IDeserializer
{
    public T Deserialize<T>(string data, string separator = ",") where T : new()
    {
        var type = typeof(T);
        var properties = type.GetProperties();
        var lines = data.Trim().Split(["\r\n", "\n"], StringSplitOptions.RemoveEmptyEntries);
        var names = lines[0].Split(",");
        var values = lines[1].Split(",");
        var result = new T();

        if (names.Length != values.Length)
            throw new ArgumentException("Wrong file");
        
        for (int i = 0; i < names.Length; i++)
        {
            var property = properties.FirstOrDefault(p => p.Name == names[i]);
            if (property == null) continue;

            var stringValue = values[i];
            var convertedValue = converter.Convert(stringValue, property.PropertyType);
            property.SetValue(result, convertedValue);
        }

        return result;
    }
}