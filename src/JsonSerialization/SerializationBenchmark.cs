using BenchmarkDotNet.Attributes;

namespace JsonSerialization;

[MemoryDiagnoser]
public class SerializationBenchmark
{
    private readonly Person _person = new Person { Name = new Name("John Doe"), Age = 42, Tags = new []{new Tag(){Value = "xD"}}};
    
    [Benchmark]
    public void NewtonsoftBenchmark()
    {
        var json = Newtonsoft.Json.JsonConvert.SerializeObject(_person);
        Newtonsoft.Json.JsonConvert.DeserializeObject<Person>(json);
    }

    [Benchmark]
    public void SystemTextJsonBenchmark()
    {
        var json = System.Text.Json.JsonSerializer.Serialize(_person);
        System.Text.Json.JsonSerializer.Deserialize<Person>(json);
    }

    [Benchmark]
    public void SystemTextJsonSourceGenBenchmark()
    {
        var json = System.Text.Json.JsonSerializer.Serialize(_person, BenchmarkJsonContext.Default.Person);
        System.Text.Json.JsonSerializer.Deserialize<Person>(json, BenchmarkJsonContext.Default.Person);
    }
}