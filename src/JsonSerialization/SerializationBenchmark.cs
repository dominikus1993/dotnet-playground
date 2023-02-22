using System.Text.Json;

using BenchmarkDotNet.Attributes;

namespace JsonSerialization;

[MemoryDiagnoser]
public class SerializationBenchmark
{
    private readonly JsonSerializerOptions Options = JsonSerializerOptions.Default;
    private readonly Person _person = new Person { Name = new Name("John Doe"), Age = 42, Tags = new []{new Tag(){Value = "xD"}}};
    
    [Benchmark]
    public Person NewtonsoftBenchmark()
    {
        var json = Newtonsoft.Json.JsonConvert.SerializeObject(_person);
        return Newtonsoft.Json.JsonConvert.DeserializeObject<Person>(json);
    }

    [Benchmark]
    public Person SystemTextJsonBenchmark()
    {
        var json = System.Text.Json.JsonSerializer.Serialize(_person, Options);
        return System.Text.Json.JsonSerializer.Deserialize<Person>(json, Options);
    }

    [Benchmark]
    public Person SystemTextJsonSourceGenBenchmark()
    {
        var json = System.Text.Json.JsonSerializer.Serialize(_person, BenchmarkJsonContext.Default.Person);
        return System.Text.Json.JsonSerializer.Deserialize<Person>(json, BenchmarkJsonContext.Default.Person);
    }
}