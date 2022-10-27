using System.Text.Json.Serialization;

namespace JsonSerialization;

[JsonSerializable(typeof(Person))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal partial class BenchmarkJsonContext : JsonSerializerContext
{
}

public class Tag
{
    public string Value { get; set; }
}
public class Person
{
    public string? Name { get; set; }
    public int Age { get; set; }
    
    public IReadOnlyList<Tag> Tags { get; set; }
}