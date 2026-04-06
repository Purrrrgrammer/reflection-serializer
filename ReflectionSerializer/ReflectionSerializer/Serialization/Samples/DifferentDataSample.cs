namespace ReflectionSerializer.Serialization.Samples;

internal sealed class DifferentDataSample
{
    public int I1 { get; init; } 
    public DateTime Date1 { get; init; } 
    public double Double1 { get; init; } 
    public float Float1 { get; init; } 
    public string? String1 { get; init; }
    public Status Status { get; init; }
    // public List<int>? List1 { get; init; }
    // public F? F1 { get; init; }
}

public enum Status
{
   NoStarted, 
   InProgress,
   Ready
}
// var sample = new DifferentDataSample()
// {
//     I1 = 1,
//     Date1 = DateTime.Now,
//     Double1 = 23.45,
//     Float1 = (float)46.45,
//     String1 = "testString\"",
//     Status = Status.Ready
// };