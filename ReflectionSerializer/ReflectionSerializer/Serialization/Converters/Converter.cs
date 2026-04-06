namespace ReflectionSerializer.Serialization.Converters;

internal sealed class Converter(IConvertersRegistry convertersRegistry) : IConverter
{
    public string Convert(object? value, Type fromType)
    {
        if (convertersRegistry.TryGetConverter(fromType, out var converter))
        {
            return converter!.Convert(value);
        }

        throw new KeyNotFoundException($"converter for type {fromType} not found");
    }

    public object? Convert(string value, Type toType)
    {
        if (convertersRegistry.TryGetConverter(toType, out var converter))
        {
            return converter!.Convert(value);
        }

        throw new KeyNotFoundException($"converter for type {toType} not found");
    }
}