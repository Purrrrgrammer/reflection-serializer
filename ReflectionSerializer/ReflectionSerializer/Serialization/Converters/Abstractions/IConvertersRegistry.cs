namespace ReflectionSerializer.Serialization.Converters;

public interface IConvertersRegistry
{
    public static IConvertersRegistry GetInstance()
    {
        throw new NotImplementedException();
    }
    
    public void AddConverter(IConcreteConverter concreteConverter);
    public bool TryGetConverter(Type type, out IConcreteConverter? concreteConverter);
}