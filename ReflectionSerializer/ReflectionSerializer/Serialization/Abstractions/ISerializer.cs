namespace ReflectionSerializer.Serialization.Abstractions;

public interface ISerializer
{
    string Serialize<T>(T serializableObject, string separator = ",") where T : new();
}