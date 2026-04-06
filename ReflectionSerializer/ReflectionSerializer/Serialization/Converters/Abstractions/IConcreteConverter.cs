namespace ReflectionSerializer.Serialization.Converters;

public interface IConcreteConverter
{
    Type TargetType { get; }
    string Convert(object? value);
    object? Convert(string value);
}