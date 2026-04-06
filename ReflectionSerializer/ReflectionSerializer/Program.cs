using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using ReflectionSerializer.Deserialization;
using ReflectionSerializer.Deserialization.Abstractions;
using ReflectionSerializer.Serialization;
using ReflectionSerializer.Serialization.Abstractions;
using ReflectionSerializer.Serialization.Converters;
using ReflectionSerializer.Serialization.Samples;

namespace ReflectionSerializer;

class Program
{
    static void Main(string[] args)
    {
        IConvertersRegistry registry = ConvertersRegistry.GetInstance();
        
        IConverter converter = new Converter(registry);
        ISerializer serializer = new CsvSerializer(converter);
        IDeserializer deserializer = new CsvDeserializer(converter);
        
        var f = new F()
        {
            I1 = 1,
            I2 = 2,
            I3 = 3,
            I4 = 4,
            I5 = 5
        };
        
        string fToDeserialize = null;
        
        using (var file = new StreamReader($"{AppDomain.CurrentDomain.BaseDirectory}/Deserialization/Samples/sampleF.csv"))
        {
            fToDeserialize = file.ReadToEnd();
        }
        
        
        int iterations = 10000;
        Console.WriteLine("----------Custom serialization--------------------");
        ShowSerializeMetrics(() =>  serializer.Serialize<F>(f), iterations);
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("----------Custom deserialization--------------------");
        ShowSerializeMetrics(() =>  deserializer.Deserialize<F>(fToDeserialize), iterations);
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("----------System.Text.Json serialization----------");
        ShowSerializeMetrics(() =>  JsonSerializer.Serialize(f), iterations);
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("----------System.Text.Json deserialization----------");
        string jsonString = JsonSerializer.Serialize(f);
        ShowSerializeMetrics(() =>  JsonSerializer.Deserialize<F>(jsonString), iterations);
        Console.WriteLine("--------------------------------------------------");
        
        Console.WriteLine("----------Output----------------------------------");
        ShowOutputMetrics(serializer.Serialize<F>(f));
        Console.WriteLine("--------------------------------------------------");
    }

    private static void ShowOutputMetrics(string result)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        Console.WriteLine(result);
        stopwatch.Stop();
        Console.WriteLine($"Time to output in console: {stopwatch.Elapsed.TotalMilliseconds} ms");
    }
    
    private static void ShowSerializeMetrics<T>(Func<T> operation, int iterations)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        for (int i = 0; i < iterations; i++)
        {
            operation();
        }
        
        stopwatch.Stop();
        
        Console.WriteLine($"Total: {stopwatch.Elapsed.TotalMilliseconds} ms for {iterations} iterations");
        Console.WriteLine($"Average: {stopwatch.Elapsed.TotalMilliseconds/iterations} ms");
    }
}