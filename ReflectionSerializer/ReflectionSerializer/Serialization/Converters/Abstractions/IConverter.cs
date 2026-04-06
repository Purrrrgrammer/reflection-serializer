namespace ReflectionSerializer.Serialization.Converters;

public interface IConverter
{
    string Convert(object? value, Type fromType);
    object? Convert(string value, Type toType);
}