namespace ReflectionSerializer.Deserialization.Abstractions;

public interface IDeserializer
{
    public T Deserialize<T>(string data, string separator = ",") where T : new();
}